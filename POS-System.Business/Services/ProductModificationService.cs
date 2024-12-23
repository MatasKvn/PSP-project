﻿using AutoMapper;
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
    public class ProductModificationService(IUnitOfWork _unitOfWork, IManyToManyService<ProductModification, CartItem, ProductModificationOnCartItem> _productModificationOnCartItemService, IMapper _mapper) : IProductModificationService
    {
        public async Task<ProductModificationResponse> CreateProductModificationAsync(ProductModificationRequest? productModificationDto, CancellationToken cancellationToken)
        {
            if (productModificationDto is null)
            {
                throw new BadRequestException(ApplicationMessages.BAD_REQUEST_MESSAGE);
            }

            var productModification = _mapper.Map<ProductModification>(productModificationDto);
            
            productModification.Version = DateTime.UtcNow;
            productModification.IsDeleted = false;

            await _unitOfWork.ProductModificationRepository.CreateAsync(productModification);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseProdModDto = _mapper.Map<ProductModificationResponse>(productModification);
            return responseProdModDto;
        }

        public async Task<ProductModificationResponse> DeleteProductModificationByIdAsync(int id, CancellationToken cancellationToken)
        {
            var productModification = await _unitOfWork.ProductModificationRepository.GetByIdAsync(id, cancellationToken);

            if (productModification is null)
            {
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            }

            productModification.IsDeleted = true;
            await _productModificationOnCartItemService.MarkActiveLinksDeletedAsync(_unitOfWork.ProductModificationOnCartItemRepository, id, true, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseProdModDto = _mapper.Map<ProductModificationResponse>(productModification);
            return responseProdModDto;
        }

        public async Task<PagedResponse<ProductModificationResponse?>> GetProductModificationsAsync(int pageSize, int pageNumber, bool? onlyActive, CancellationToken cancellationToken)
        {
            var (productModifications, totalCount) = await _unitOfWork.ProductModificationRepository.GetByExpressionWithPaginationAsync(
                onlyActive is null ? null : x => x.IsDeleted != onlyActive,
                pageSize,
                pageNumber,
                cancellationToken
            );

            if (productModifications is null)
            {
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            }

            var prodModDtos = _mapper.Map<List<ProductModificationResponse>>(productModifications);

            return new PagedResponse<ProductModificationResponse?>(totalCount, pageSize, pageNumber, prodModDtos);
        }

        public async Task<ProductModificationResponse?> GetProductModificationByIdAsync(int id, CancellationToken cancellationToken)
        {
            var productModification = await _unitOfWork.ProductModificationRepository.GetByIdAsync(id, cancellationToken);

            if (productModification is null)
            {
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            }

            var prodModDto = _mapper.Map<ProductModificationResponse>(productModification);
            return prodModDto;
        }

        public async Task<IEnumerable<ProductModificationResponse?>> GetProductModificationVersionsByProductModificationIdAsync(int productModificationId, CancellationToken cancellationToken)
        {
            var productModifications = await _unitOfWork.ProductModificationRepository.GetAllByExpressionWithIncludesAsync(
                p => p.ProductModificationId == productModificationId,
                cancellationToken
            );

            if (productModifications is null)
            {
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            }

            var prodModDtos = _mapper.Map<List<ProductModificationResponse>>(productModifications);
            return prodModDtos;
        }

        public async Task<ProductModificationResponse> UpdateProductModificationByIdAsync(int id, ProductModificationRequest? productModificationDto, CancellationToken cancellationToken)
        {
            if (productModificationDto is null)
            {
                throw new BadRequestException(ApplicationMessages.BAD_REQUEST_MESSAGE);
            }

            var currentProdMod = await _unitOfWork.ProductModificationRepository.GetByIdAsync(id, cancellationToken);

            if (currentProdMod is null)
            {
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            }

            currentProdMod.IsDeleted = true;

            var newProdMod = _mapper.Map<ProductModification>(productModificationDto);
            newProdMod.ProductModificationId = currentProdMod.ProductModificationId;
            newProdMod.ProductVersionId = currentProdMod.ProductVersionId;
            newProdMod.Version = DateTime.UtcNow;
            newProdMod.IsDeleted = false;

            await _unitOfWork.ProductModificationRepository.CreateAsync(newProdMod, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _productModificationOnCartItemService.RelinkItemToItemAsync(_unitOfWork.ProductModificationOnCartItemRepository, id, newProdMod.Id, true, cancellationToken);

            var responseProdModDto = _mapper.Map<ProductModificationResponse>(newProdMod);
            return responseProdModDto;
        }

        public async Task<IEnumerable<ProductModificationResponse>> GetProductModificationsLinkedToCartItemId(int cartItemId, DateTime? timeStamp, CancellationToken cancellationToken)
        {
            IEnumerable<int> prodModLinkIds;
            IList<ProductModification> productModifications = new List<ProductModification>();

            prodModLinkIds = await _productModificationOnCartItemService.GetLinkIdsAsync(_unitOfWork.ProductModificationOnCartItemRepository, cartItemId, false, timeStamp, cancellationToken);

            foreach (var productModificationId in prodModLinkIds)
            {
                var productModification = await _unitOfWork.ProductModificationRepository.GetByIdAsync(productModificationId, cancellationToken);

                if (productModification is not null)
                    productModifications.Add(productModification);
            }

            var productModificationDtos = _mapper.Map<List<ProductModificationResponse>>(productModifications);
            return productModificationDtos;
        }

        public async Task<PagedResponse<ProductModificationResponse?>> GetProductModificationsLinkedToProductId(int pageSize, int pageNumber, int productId, CancellationToken cancellationToken)
        {
            var (prodMods, totalCount) = await _unitOfWork.ProductModificationRepository.GetAllByExpressionWithIncludesAndPaginationAsync(
                x => x.IsDeleted == false && x.ProductVersionId == productId,
                pageSize,
                pageNumber,
                cancellationToken
            );

            var prodModDtos = _mapper.Map<List<ProductModificationResponse>>(prodMods);
            return new PagedResponse<ProductModificationResponse?>(totalCount, pageSize, pageNumber, prodModDtos);
        }
    }
}
