using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface IProductService
    {
        public Task<PagedResponse<ProductResponse?>> GetProductsAsync(int pageSize, int pageNumber, bool? onlyActive, CancellationToken cancellationToken);
        public Task<ProductResponse?> GetProductByProductIdAsync(int productId, CancellationToken cancellationToken);
        public Task<IEnumerable<ProductResponse?>> GetProductVersionsByProductIdAsync(int productId, CancellationToken cancellationToken);
        public Task<ProductResponse> CreateProductAsync(ProductRequest? productDto, CancellationToken cancellationToken);
        public Task<ProductResponse> UpdateProductByProductIdAsync(int productId, ProductRequest? productDto, CancellationToken cancellationToken);
        public Task<ProductResponse> DeleteProductByProductIdAsync(int productId, CancellationToken cancellationToken);
    }
}
