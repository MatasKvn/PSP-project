using POS_System.Business.Dtos.Tax;

namespace POS_System.Business.Services.Interfaces
{
    public interface ITaxService
    {
        public Task<IEnumerable<TaxResponseDto>> GetAllTaxesAsync(CancellationToken cancellationToken);
        public Task<TaxResponseDto> CreateTaxAsync(TaxRequestDto taxDto, CancellationToken cancellationToken);
        public Task DeleteTaxAsync(int id, CancellationToken cancellationToken);
        public Task<TaxResponseDto> UpdateTaxAsync(int id, TaxRequestDto taxDto, CancellationToken cancellationToken);
        public Task<TaxResponseDto> GetTaxByIdAsync(int id, CancellationToken cancellationToken);
    }
}
