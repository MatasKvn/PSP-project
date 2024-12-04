using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductResponse?>> GetAllProductsAsync(CancellationToken cancellationToken);
        public Task<ProductResponse?> GetProductByProductIdAsync(int productId, CancellationToken cancellationToken);
        public Task<IEnumerable<ProductResponse?>> GetProductVersionsByProductIdAsync(int productId, CancellationToken cancellationToken);
        public Task<ProductResponse> CreateProductAsync(ProductRequest? productDto, CancellationToken cancellationToken);
        public Task<ProductResponse> UpdateProductByProductIdAsync(int productId, ProductRequest? productDto, CancellationToken cancellationToken);
        public Task<ProductResponse> DeleteProductByProductIdAsync(int productId, CancellationToken cancellationToken);
    }
}
