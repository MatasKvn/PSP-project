using POS_System.Business.Dtos;

namespace POS_System.Business.Services.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDto?>> GetAllProductsAsync(CancellationToken cancellationToken);
        public Task<ProductDto?> GetProductByIdAsync(int productId, CancellationToken cancellationToken);
        public Task AddProductAsync(ProductDto productDto, CancellationToken cancellationToken);
        public Task RemoveProductAsync(ProductDto productDto, CancellationToken cancellationToken);
        public Task RemoveProductByIdAsync(int productId, CancellationToken cancellationToken);
    }
}
