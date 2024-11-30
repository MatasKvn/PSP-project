using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface IProductModificationService
    {
        public Task<IEnumerable<ProductModificationResponse?>> GetAllProductModificationsAsync(CancellationToken cancelationToken);
        public Task<ProductModificationResponse?> GetProductModificationByProductModificationIdAsync(int productModificationId , CancellationToken cancelationToken);
        public Task<IEnumerable<ProductModificationResponse?>> GetProductModificationVersionsByProductModificationIdAsync(int productModificationId,  CancellationToken cancelationToken);
        public Task<ProductModificationResponse> CreateProductModificationAsync(ProductModificationRequest? productModifictaionDto, CancellationToken cancelationToken);
        public Task<ProductModificationResponse> UpdateProductModificationAsync(int productModificationId, ProductModificationRequest? productModifictaionDto, CancellationToken cancellationToken);
        public Task<ProductModificationResponse> DeleteProductModificationAsync(int productModificationId, CancellationToken cancellationToken);
    }
}
