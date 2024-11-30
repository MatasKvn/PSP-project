using AutoMapper;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;
using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services
{
    public class ProductModificationService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductModificationService
    {
        public async Task<ProductModificationResponse> CreateProductModificationAsync(ProductModificationRequest? productModifictaionDto, CancellationToken cancelationToken)
        {
            var productModification = _mapper.Map<ProductModification>(productModifictaionDto);
            
            productModification.Version = DateTime.Now;
            productModification.IsDeleted = false;

            await _unitOfWork.ProductModificationRepository.CreateAsync(productModification);
            await _unitOfWork.SaveChangesAsync(cancelationToken);

            var responseProdModDto = _mapper.Map<ProductModificationResponse>(productModification);
            return responseProdModDto;
        }

        public async Task<ProductModificationResponse> DeleteProductModificationAsync(int productModificationId, CancellationToken cancellationToken)
        {
            var productModification = await _unitOfWork.ProductModificationRepository.GetByExpressionWithIncludesAsync(
                p => p.ProductModificationId == productModificationId,
                cancellationToken
            );

            productModification.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseProdModDto = _mapper.Map<ProductModificationResponse>(productModification);
            return responseProdModDto;
        }

        public async Task<IEnumerable<ProductModificationResponse?>> GetAllProductModificationsAsync(CancellationToken cancelationToken)
        {
            var productModifications = await _unitOfWork.ProductModificationRepository.GetAllByExpressionAsync(
                p => !p.IsDeleted,
                cancelationToken
            );

            var prodModDtos = _mapper.Map<List<ProductModificationResponse?>>(productModifications);

            return prodModDtos;
        }

        public async Task<ProductModificationResponse?> GetProductModificationByProductModificationIdAsync(int productModificationId, CancellationToken cancelationToken)
        {
            var productModification = await _unitOfWork.ProductModificationRepository.GetByExpressionWithIncludesAsync(
                p => p.ProductModificationId == productModificationId && !p.IsDeleted,
                cancelationToken
            );

            var prodModDto = _mapper.Map<ProductModificationResponse>(productModification);
            return prodModDto;
        }

        public async Task<IEnumerable<ProductModificationResponse?>> GetProductModificationVersionsByProductModificationIdAsync(int productModificationId, CancellationToken cancelationToken)
        {
            var productModifications = await _unitOfWork.ProductModificationRepository.GetAllByExpressionWithIncludesAsync(
                p => p.ProductModificationId == productModificationId,
                cancelationToken
            );

            var prodModDtos = _mapper.Map<List<ProductModificationResponse>>(productModifications);
            return prodModDtos;
        }

        public async Task<ProductModificationResponse> UpdateProductModificationAsync(int productModificationId, ProductModificationRequest? productModifictaionDto, CancellationToken cancellationToken)
        {
            var currentProdMod = await _unitOfWork.ProductModificationRepository.GetByExpressionWithIncludesAsync(
                p => p.ProductModificationId == productModificationId && !p.IsDeleted,
                cancellationToken
            );

            currentProdMod.IsDeleted = true;

            var newProdMod = new ProductModification
            {
                ProductModificationId = productModificationId,
                ProductVersionId = productModifictaionDto.ProductVersionId,
                Name = productModifictaionDto.Name,
                Description = productModifictaionDto.Description,
                Price = productModifictaionDto.Price,
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
