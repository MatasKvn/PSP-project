using AutoMapper;
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
    public class ProductModificationService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductModificationService
    {
        public async Task<ProductModificationResponse> CreateProductModificationAsync(ProductModificationRequest? productModificationDto, CancellationToken cancellationToken)
        {
            if (productModificationDto is null)
            {
                throw new BadRequestException(ApplicationMesssages.BAD_REQUEST_MESSAGE);
            }

            var productModification = _mapper.Map<ProductModification>(productModificationDto);
            
            productModification.Version = DateTime.Now;
            productModification.IsDeleted = false;

            await _unitOfWork.ProductModificationRepository.CreateAsync(productModification);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseProdModDto = _mapper.Map<ProductModificationResponse>(productModification);
            return responseProdModDto;
        }

        public async Task<ProductModificationResponse> DeleteProductModificationAsync(int productModificationId, CancellationToken cancellationToken)
        {
            var productModification = await _unitOfWork.ProductModificationRepository.GetByExpressionWithIncludesAsync(
                p => p.ProductModificationId == productModificationId,
                cancellationToken
            );

            if (productModification is null)
            {
                throw new NotFoundException(ApplicationMesssages.NOT_FOUND_ERROR);
            }

            productModification.IsDeleted = true;
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
                throw new NotFoundException(ApplicationMesssages.NOT_FOUND_ERROR);
            }

            var prodModDtos = _mapper.Map<List<ProductModificationResponse>>(productModifications);

            return new PagedResponse<ProductModificationResponse?>(totalCount, pageSize, pageNumber, prodModDtos);
        }

        public async Task<ProductModificationResponse?> GetProductModificationByProductModificationIdAsync(int productModificationId, CancellationToken cancellationToken)
        {
            var productModification = await _unitOfWork.ProductModificationRepository.GetByExpressionWithIncludesAsync(
                p => p.ProductModificationId == productModificationId && !p.IsDeleted,
                cancellationToken
            );

            if (productModification is null)
            {
                throw new NotFoundException(ApplicationMesssages.NOT_FOUND_ERROR);
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
                throw new NotFoundException(ApplicationMesssages.NOT_FOUND_ERROR);
            }

            var prodModDtos = _mapper.Map<List<ProductModificationResponse>>(productModifications);
            return prodModDtos;
        }

        public async Task<ProductModificationResponse> UpdateProductModificationAsync(int productModificationId, ProductModificationRequest? productModificationDto, CancellationToken cancellationToken)
        {
            if (productModificationDto is null)
            {
                throw new BadRequestException(ApplicationMesssages.BAD_REQUEST_MESSAGE);
            }

            var currentProdMod = await _unitOfWork.ProductModificationRepository.GetByExpressionWithIncludesAsync(
                p => p.ProductModificationId == productModificationId && !p.IsDeleted,
                cancellationToken
            );

            if (currentProdMod is null)
            {
                throw new NotFoundException(ApplicationMesssages.NOT_FOUND_ERROR);
            }

            currentProdMod.IsDeleted = true;

            var newProdMod = new ProductModification
            {
                ProductModificationId = productModificationId,
                ProductVersionId = productModificationDto.ProductVersionId,
                Name = productModificationDto.Name,
                Description = productModificationDto.Description,
                Price = productModificationDto.Price,
                Version = DateTime.Now,
                IsDeleted = false
            };

            await _unitOfWork.ProductModificationRepository.CreateAsync(newProdMod, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseProdModDto = _mapper.Map<ProductModificationResponse>(newProdMod);
            return responseProdModDto;
        }
    }
}
