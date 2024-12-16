using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface IProductModificationService
    {
        public Task<PagedResponse<ProductModificationResponse?>> GetProductModificationsAsync(int pageSize, int pageNumber, bool? onlyActive, CancellationToken cancellationToken);
        public Task<ProductModificationResponse?> GetProductModificationByIdAsync(int id , CancellationToken cancellationToken);
        public Task<IEnumerable<ProductModificationResponse?>> GetProductModificationVersionsByProductModificationIdAsync(int productModificationId,  CancellationToken cancellationToken);
        public Task<ProductModificationResponse> CreateProductModificationAsync(ProductModificationRequest? productModificationDto, CancellationToken cancellationToken);
        public Task<ProductModificationResponse> UpdateProductModificationByIdAsync(int id, ProductModificationRequest? productModificationDto, CancellationToken cancellationToken);
        public Task<ProductModificationResponse> DeleteProductModificationByIdAsync(int id, CancellationToken cancellationToken);
        public Task<IEnumerable<ProductModificationResponse>> GetProductModificationsLinkedToCartItemId(int cartItemId, DateTime? timeStamp, CancellationToken cancellationToken);
        public Task<PagedResponse<ProductModificationResponse?>> GetProductModificationsLinkedToProductId(int pageSize, int pageNumber, int productId, CancellationToken cancellationToken);
    }
}
