using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface ITaxService
    {
        public Task<IEnumerable<TaxResponse>> GetAllTaxesAsync(int pageSize, int pageNumber, CancellationToken cancellationToken);
        public Task<TaxResponse> CreateTaxAsync(TaxRequest taxDto, CancellationToken cancellationToken);
        public Task DeleteTaxAsync(int id, CancellationToken cancellationToken);
        public Task<TaxResponse> UpdateTaxAsync(int id, TaxRequest taxDto, CancellationToken cancellationToken);
        public Task<TaxResponse> GetTaxByIdAsync(int id, CancellationToken cancellationToken);
        public Task LinkTaxToItemsAsync(int taxId, bool areItemsProducts, int[] itemIdList, CancellationToken cancellationToken);
        public Task UnlinkTaxFromItemsAsync(int taxId, bool areItemsProducts, int[] itemIdList, CancellationToken cancellationToken);
        public Task<IEnumerable<TaxResponse>> GetTaxesLinkedToItemId(int itemId, bool isProduct, DateTime? timeStamp, CancellationToken cancellationToken);
    }
}
