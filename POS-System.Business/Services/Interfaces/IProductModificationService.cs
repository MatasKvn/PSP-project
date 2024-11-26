using POS_System.Business.Dtos.ProductModificationDtos;

namespace POS_System.Business.Services.Interfaces
{
    public interface IProductModificationService
    {
        public Task<IEnumerable<GetProductModificationDto?>> GetAllProductModificationsAsync(CancellationToken cancelationToken);
        public Task<GetProductModificationDto?> GetProductModificationByProductModificationIdAsync(int productModificationId , CancellationToken cancelationToken);
        public Task<IEnumerable<GetProductModificationDto?>> GetProductModificationVersionsByProductModificationIdAsync(int productModificationId,  CancellationToken cancelationToken);
        public Task<GetProductModificationDto> CreateProductModificationAsync(CreateProductModificationDto? productModifictaionDto, CancellationToken cancelationToken);
        public Task<GetProductModificationDto> UpdateProductModificationAsync(int productModificationId, CreateProductModificationDto? productModifictaionDto, CancellationToken cancellationToken);
        public Task<GetProductModificationDto> DeleteProductModificationAsync(int productModificationId, CancellationToken cancellationToken);
    }
}
