using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface IProductModificationService
    {
        public Task<PagedResponse<ProductModificationResponse?>> GetProductModificationsAsync(int pageSize, int pageNumber, bool? onlyActive, CancellationToken cancellationToken);
        public Task<ProductModificationResponse?> GetProductModificationByProductModificationIdAsync(int productModificationId , CancellationToken cancellationToken);
        public Task<IEnumerable<ProductModificationResponse?>> GetProductModificationVersionsByProductModificationIdAsync(int productModificationId,  CancellationToken cancellationToken);
        public Task<ProductModificationResponse> CreateProductModificationAsync(ProductModificationRequest? productModificationDto, CancellationToken cancellationToken);
        public Task<ProductModificationResponse> UpdateProductModificationAsync(int productModificationId, ProductModificationRequest? productModificationDto, CancellationToken cancellationToken);
        public Task<ProductModificationResponse> DeleteProductModificationAsync(int productModificationId, CancellationToken cancellationToken);
    }
}
