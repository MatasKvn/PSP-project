using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface IProductService
    {
        public Task<PagedResponse<ProductResponse?>> GetProductsAsync(int pageSize, int pageNumber, bool? onlyActive, CancellationToken cancellationToken);
        public Task<ProductResponse?> GetProductByIdAsync(int id, CancellationToken cancellationToken);
        public Task<IEnumerable<ProductResponse?>> GetProductVersionsByProductIdAsync(int productId, CancellationToken cancellationToken);
        public Task<ProductResponse> CreateProductAsync(ProductRequest? productDto, CancellationToken cancellationToken);
        public Task<ProductResponse> UpdateProductByIdAsync(int id, ProductRequest? productDto, CancellationToken cancellationToken);
        public Task<ProductResponse> DeleteProductByIdAsync(int id, CancellationToken cancellationToken);
        public Task LinkProductToTaxesAsync(int productId, int[] taxIdList, CancellationToken cancellationToken);
        public Task UnlinkProductFromTaxesAsync(int productId, int[] taxIdList, CancellationToken cancellationToken);
        public Task<IEnumerable<ProductResponse>> GetProductsLinkedToTaxId(int taxId, DateTime? timeStamp, CancellationToken cancellationToken);
    }
}
