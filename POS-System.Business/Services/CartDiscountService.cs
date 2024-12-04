using AutoMapper;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;
using POS_System.Business.Services.Interfaces;
using POS_System.Common.Constants;
using POS_System.Common.Exceptions;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services
{
    public class CartDiscountService(IUnitOfWork _unitOfWork, IManyToManyService<Cart, CartDiscount, CartOnCartDiscount> _cartOnCartDiscountService, IMapper _mapper) : ICartDiscountService
    {
        public async Task<IEnumerable<CartDiscountResponse>> GetAllCartDiscountsAsync(CancellationToken cancellationToken)
        {
            var cartDiscounts = await _unitOfWork.CartDiscountRepository.GetAllByExpressionAsync(x => x.IsDeleted == false);

            //If item discount is past expiration date
            //We do not show it and mark as deleted
            //Only way to do it without periodic checking is to do it when we do a call
            for (var i = cartDiscounts.Count - 1; i >= 0; --i)
            {
                var cartDiscount = cartDiscounts[i];
                if (cartDiscount.EndDate <= DateTime.UtcNow)
                {
                    await DeleteCartDiscountAsync(cartDiscount.Id, cancellationToken);
                    cartDiscounts.Remove(cartDiscount);
                }
            }

            var cartDiscountDtos = _mapper.Map<List<CartDiscountResponse>>(cartDiscounts);
            return cartDiscountDtos;
        }

        public async Task<CartDiscountResponse> CreateCartDiscountAsync(CartDiscountRequest cartDiscountDto, CancellationToken cancellationToken)
        {
            var cartDiscount = _mapper.Map<CartDiscount>(cartDiscountDto);
            cartDiscount.IsDeleted = false;
            cartDiscount.Version = DateTime.UtcNow;

            await _unitOfWork.CartDiscountRepository.CreateAsync(cartDiscount);
            await _unitOfWork.SaveChangesAsync();

            var responseCartDiscountDto = _mapper.Map<CartDiscountResponse>(cartDiscount);
            return responseCartDiscountDto;
        }

        public async Task DeleteCartDiscountAsync(int id, CancellationToken cancellationToken)
        {
            var cartDiscount = await _unitOfWork.CartDiscountRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);

            cartDiscount.IsDeleted = true;
            await _cartOnCartDiscountService.MarkActiveLinksDeletedAsync(_unitOfWork.CartOnCartDiscountRepository, id, false, cancellationToken);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<CartDiscountResponse> UpdateCartDiscountAsync(int id, CartDiscountRequest cartDiscountDto, CancellationToken cancellationToken)
        {
            var currentCartDiscount = await _unitOfWork.CartDiscountRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);

            currentCartDiscount.IsDeleted = true;

            var newCartDiscount = _mapper.Map<CartDiscount>(cartDiscountDto);
            newCartDiscount.CartDiscountId = currentCartDiscount.CartDiscountId;
            newCartDiscount.IsDeleted = false;
            newCartDiscount.Version = DateTime.UtcNow;

            await _unitOfWork.CartDiscountRepository.CreateAsync(newCartDiscount, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            var newCartDiscountDto = _mapper.Map<CartDiscountResponse>(newCartDiscount);

            await _cartOnCartDiscountService.RelinkItemToItem(_unitOfWork.CartOnCartDiscountRepository, id, newCartDiscount.Id, false, cancellationToken);

            return newCartDiscountDto;
        }

        public async Task<CartDiscountResponse> GetCartDiscountByIdAsync(int id, CancellationToken cancellationToken)
        {
            var cartDiscount = await _unitOfWork.CartDiscountRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);

            var cartDiscountDto = _mapper.Map<CartDiscountResponse>(cartDiscount);
            return cartDiscountDto;
        }

        public async Task LinkCartDiscountToCartsAsync(int cartDiscountId, int[] cartIdList, CancellationToken cancellationToken)
        {
            await _cartOnCartDiscountService.LinkItemToItemsAsync(_unitOfWork.CartRepository, _unitOfWork.CartDiscountRepository, _unitOfWork.CartOnCartDiscountRepository, cartDiscountId, cartIdList, false, cancellationToken);
        }

        public async Task UnlinkCartDiscountFromItemsAsync(int cartDiscountId, int[] cartIdList, CancellationToken cancellationToken)
        {
            await _cartOnCartDiscountService.UnlinkItemFromItemsAsync(_unitOfWork.CartRepository, _unitOfWork.CartDiscountRepository, _unitOfWork.CartOnCartDiscountRepository, cartDiscountId, cartIdList, false, cancellationToken);
        }


        public async Task<IEnumerable<CartDiscountResponse>> GetCartDiscountsLinkedToCartId(int cartId, DateTime? timeStamp, CancellationToken cancellationToken)
        {
            IEnumerable<int> cartDiscountsLinkIds;
            IList<CartDiscount> cartDiscounts = new List<CartDiscount>();

            cartDiscountsLinkIds = await _cartOnCartDiscountService.GetLinkIdsAsync(_unitOfWork.CartOnCartDiscountRepository, cartId, true, timeStamp, cancellationToken);

            foreach (var cartDiscountId in cartDiscountsLinkIds)
            {
                var cartDiscount = await _unitOfWork.CartDiscountRepository.GetByIdAsync(cartDiscountId, cancellationToken);

                if (cartDiscount is not null)
                {
                    //Extra checks for time limited item discounts
                    if ((cartDiscount.StartDate is null && cartDiscount.EndDate is null) || (timeStamp >= cartDiscount.StartDate && timeStamp <= cartDiscount.EndDate))
                        cartDiscounts.Add(cartDiscount);
                }
            }

            var cartDiscountDtos = _mapper.Map<List<CartDiscountResponse>>(cartDiscounts);
            return cartDiscountDtos;
        }
    }
}