using POS_System.Business.Dtos.ProductDtos;

namespace POS_System.Business.Services.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<GetProductDto?>> GetAllProductsAsync(CancellationToken cancellationToken);
        public Task<GetProductDto?> GetProductByProductIdAsync(int productId, CancellationToken cancellationToken);
        public Task<IEnumerable<GetProductDto?>> GetProductVersionsByProductIdAsync(int productId, CancellationToken cancellationToken);
        public Task<GetProductDto> CreateProductAsync(CreateProductDto? productDto, CancellationToken cancellationToken);
        public Task<GetProductDto> UpdateProductByProductIdAsync(int productId, CreateProductDto? productDto, CancellationToken cancellationToken);
        public Task DeleteProductByProductIdAsync(int productId, CancellationToken cancellationToken);
    }
}
