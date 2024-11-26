using AutoMapper;
using POS_System.Business.Dtos.ProductModificationDtos;
using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services
{
    public class ProductModificationService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductModificationService
    {
        public async Task<GetProductModificationDto> CreateProductModificationAsync(CreateProductModificationDto? productModifictaionDto, CancellationToken cancelationToken)
        {
            var newProductModificationId = await _unitOfWork.ProductModificationRepository.GetMaxProductModificationIdAsync(cancelationToken) + 1;

            var productModification = _mapper.Map<ProductModification>(productModifictaionDto);
            
            productModification.ProductModificationId = newProductModificationId;
            productModification.Version = DateTime.Now;
            productModification.IsDeleted = false;

            await _unitOfWork.ProductModificationRepository.CreateAsync(productModification);
            await _unitOfWork.SaveChangesAsync(cancelationToken);

            var responseProdModDto = _mapper.Map<GetProductModificationDto>(productModification);
            return responseProdModDto;
        }

        public async Task<GetProductModificationDto> DeleteProductModificationAsync(int productModificationId, CancellationToken cancellationToken)
        {
            var productModification = await _unitOfWork.ProductModificationRepository.GetByExpressionWithIncludesAsync(
                p => p.ProductModificationId == productModificationId,
                cancellationToken
            );

            productModification.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseProdModDto = _mapper.Map<GetProductModificationDto>(productModification);
            return responseProdModDto;
        }

        public async Task<IEnumerable<GetProductModificationDto?>> GetAllProductModificationsAsync(CancellationToken cancelationToken)
        {
            var productModifications = await _unitOfWork.ProductModificationRepository.GetAllByExpressionAsync(
                p => !p.IsDeleted,
                cancelationToken
            );

            var prodModDtos = _mapper.Map<List<GetProductModificationDto?>>(productModifications);

            return prodModDtos;
        }

        public async Task<GetProductModificationDto?> GetProductModificationByProductModificationIdAsync(int productModificationId, CancellationToken cancelationToken)
        {
            var productModification = await _unitOfWork.ProductModificationRepository.GetByExpressionWithIncludesAsync(
                p => p.ProductModificationId == productModificationId && !p.IsDeleted,
                cancelationToken
            );

            var prodModDto = _mapper.Map<GetProductModificationDto>(productModification);
            return prodModDto;
        }

        public async Task<IEnumerable<GetProductModificationDto?>> GetProductModificationVersionsByProductModificationIdAsync(int productModificationId, CancellationToken cancelationToken)
        {
            var productModifications = await _unitOfWork.ProductModificationRepository.GetAllByExpressionWithIncludesAsync(
                p => p.ProductModificationId == productModificationId,
                cancelationToken
            );

            var prodModDtos = _mapper.Map<List<GetProductModificationDto>>(productModifications);
            return prodModDtos;
        }

        public async Task<GetProductModificationDto> UpdateProductModificationAsync(int productModificationId, CreateProductModificationDto? productModifictaionDto, CancellationToken cancellationToken)
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

            var responseProdModDto = _mapper.Map<GetProductModificationDto>(newProdMod);
            return responseProdModDto;
        }
    }
}
