using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;
using POS_System.Business.Services.Interfaces;
using POS_System.Common.Constants;
using POS_System.Common.Exceptions;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services
{
    public class ItemDiscountService(IUnitOfWork _unitOfWork, IManyToManyService<Product, ItemDiscount, ProductOnItemDiscount> _productOnItemDiscountService,
        IManyToManyService<Service, ItemDiscount, ServiceOnItemDiscount> _serviceOnItemDiscountService, IMapper _mapper) : IItemDiscountService
    {
        public async Task<PagedResponse<ItemDiscountResponse>> GetAllItemDiscountsAsync(CancellationToken cancellationToken, int pageNum, int pageSize)
        {
            var (discounts, totalCount) = await _unitOfWork.ItemDiscountRepository.GetAllByExpressionWithIncludesAndPaginationAsync(
                x => !x.IsDeleted && (x.EndDate >= DateTime.UtcNow || x.EndDate == null),
                pageSize,
                pageNum,
                cancellationToken
            );

            //If item discount is past expiration date
            //We do not show it and mark as deleted
            //Only way to do it without periodic checking is to do it when we do a call
            var discountsToReturn = new List<ItemDiscount>();

            foreach (var discount in discounts)
            {
                if (discount.EndDate <= DateTime.UtcNow)
                {
                    await DeleteItemDiscountAsync(discount.Id, cancellationToken);
                    continue;
                }
                discountsToReturn.Add(discount);
            }
            var itemDiscountDtos = _mapper.Map<List<ItemDiscountResponse>>(discountsToReturn);
            return new PagedResponse<ItemDiscountResponse>(totalCount, pageSize, pageNum, itemDiscountDtos);
        }

        public async Task<ItemDiscountResponse> CreateItemDiscountAsync(ItemDiscountRequest itemDiscountDto, CancellationToken cancellationToken)
        {
            var itemDiscount = _mapper.Map<ItemDiscount>(itemDiscountDto);
            itemDiscount.IsDeleted = false;
            itemDiscount.Version = DateTime.UtcNow;

            await _unitOfWork.ItemDiscountRepository.CreateAsync(itemDiscount);
            await _unitOfWork.SaveChangesAsync();

            var responseItemDiscountDto = _mapper.Map<ItemDiscountResponse>(itemDiscount);
            return responseItemDiscountDto;
        }

        public async Task DeleteItemDiscountAsync(int id, CancellationToken cancellationToken)
        {
            var itemDiscount = await _unitOfWork.ItemDiscountRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);

            itemDiscount.IsDeleted = true;
            await _productOnItemDiscountService.MarkActiveLinksDeletedAsync(_unitOfWork.ProductOnItemDiscountRepository, id, false, cancellationToken);
            await _serviceOnItemDiscountService.MarkActiveLinksDeletedAsync(_unitOfWork.ServiceOnItemDiscountRepository, id, false, cancellationToken);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ItemDiscountResponse> UpdateItemDiscountAsync(int id, ItemDiscountRequest itemDiscountDto, CancellationToken cancellationToken)
        {
            var currentItemDiscount = await _unitOfWork.ItemDiscountRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);

            currentItemDiscount.IsDeleted = true;

            var newItemDiscount = _mapper.Map<ItemDiscount>(itemDiscountDto);
            newItemDiscount.ItemDiscountId = currentItemDiscount.ItemDiscountId;
            newItemDiscount.IsDeleted = false;
            newItemDiscount.Version = DateTime.UtcNow;

            await _unitOfWork.ItemDiscountRepository.CreateAsync(newItemDiscount, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            var newItemDiscountDto = _mapper.Map<ItemDiscountResponse>(newItemDiscount);

            await _productOnItemDiscountService.RelinkItemToItemAsync(_unitOfWork.ProductOnItemDiscountRepository, id, newItemDiscount.Id, false, cancellationToken);
            await _serviceOnItemDiscountService.RelinkItemToItemAsync(_unitOfWork.ServiceOnItemDiscountRepository, id, newItemDiscount.Id, false, cancellationToken);

            return newItemDiscountDto;
        }

        public async Task<ItemDiscountResponse> GetItemDiscountByIdAsync(int id, CancellationToken cancellationToken)
        {
            var itemDiscount = await _unitOfWork.ItemDiscountRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);

            var itemDiscountDto = _mapper.Map<ItemDiscountResponse>(itemDiscount);
            return itemDiscountDto;
        }

        public async Task LinkItemDiscountToItemsAsync(int itemDiscountId, bool itemsAreProducts, int[] itemIdList, CancellationToken cancellationToken)
        {
            if (itemsAreProducts)
            {
                await _productOnItemDiscountService.LinkItemToItemsAsync(_unitOfWork.ProductRepository, _unitOfWork.ItemDiscountRepository, _unitOfWork.ProductOnItemDiscountRepository, itemDiscountId, itemIdList, false, cancellationToken);
            }
            else
            {
                await _serviceOnItemDiscountService.LinkItemToItemsAsync(_unitOfWork.ServiceRepository, _unitOfWork.ItemDiscountRepository, _unitOfWork.ServiceOnItemDiscountRepository, itemDiscountId, itemIdList, false, cancellationToken);
            }

        }

        public async Task UnlinkItemDiscountFromItemsAsync(int itemDiscountId, bool itemsAreProducts, int[] itemIdList, CancellationToken cancellationToken)
        {
            if (itemsAreProducts)
            {
                await _productOnItemDiscountService.UnlinkItemFromItemsAsync(_unitOfWork.ProductRepository, _unitOfWork.ItemDiscountRepository, _unitOfWork.ProductOnItemDiscountRepository, itemDiscountId, itemIdList, false, cancellationToken);
            }
            else
            {
                await _serviceOnItemDiscountService.UnlinkItemFromItemsAsync(_unitOfWork.ServiceRepository, _unitOfWork.ItemDiscountRepository, _unitOfWork.ServiceOnItemDiscountRepository, itemDiscountId, itemIdList, false, cancellationToken);
            }
        }


        public async Task<IEnumerable<ItemDiscountResponse>> GetItemDiscountsLinkedToItemId(int itemId, bool isProduct, DateTime? timeStamp, CancellationToken cancellationToken)
        {
            IEnumerable<int> itemDiscountsLinkIds;
            IList<ItemDiscount> itemDiscounts = new List<ItemDiscount>();

            itemDiscountsLinkIds = isProduct ?
                await _productOnItemDiscountService.GetLinkIdsAsync(_unitOfWork.ProductOnItemDiscountRepository, itemId, true, timeStamp, cancellationToken)
                : await _serviceOnItemDiscountService.GetLinkIdsAsync(_unitOfWork.ServiceOnItemDiscountRepository, itemId, true, timeStamp, cancellationToken);

            foreach (var itemDiscountId in itemDiscountsLinkIds)
            {
                var itemDiscount = await _unitOfWork.ItemDiscountRepository.GetByIdAsync(itemDiscountId, cancellationToken);

                if (itemDiscount is not null)
                {
                    //Extra checks for time limited item discounts
                    if ((itemDiscount.StartDate is null && itemDiscount.EndDate is null) || (timeStamp >= itemDiscount.StartDate && timeStamp <= itemDiscount.EndDate))
                        itemDiscounts.Add(itemDiscount);
                }
            }

            var itemDiscountDtos = _mapper.Map<List<ItemDiscountResponse>>(itemDiscounts);
            return itemDiscountDtos;
        }
    }
}
