using POS_System.Business.Dtos.Tax;

namespace POS_System.Business.Services.Interfaces
{
    public interface ITaxService
    {
        public Task<IEnumerable<TaxResponseDto>> GetAllTaxesAsync(int pageSize, int pageNumber, CancellationToken cancellationToken);
        public Task<TaxResponseDto> CreateTaxAsync(TaxRequestDto taxDto, CancellationToken cancellationToken);
        public Task DeleteTaxAsync(int id, CancellationToken cancellationToken);
        public Task<TaxResponseDto> UpdateTaxAsync(int id, TaxRequestDto taxDto, CancellationToken cancellationToken);
        public Task<TaxResponseDto> GetTaxByIdAsync(int id, CancellationToken cancellationToken);
        public Task LinkTaxToItemsAsync(int taxId, bool areItemsProducts, int[] itemIdList, CancellationToken cancellationToken);
        public Task UnlinkTaxFromItemsAsync(int taxId, bool areItemsProducts, int[] itemIdList, CancellationToken cancellationToken);
        public Task<IEnumerable<TaxResponseDto>> GetTaxesLinkedToItemId(int itemId, bool isProduct, DateTime? timeStamp, CancellationToken cancellationToken);
    }
}
