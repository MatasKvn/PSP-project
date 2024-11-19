using POS_System.Business.Dtos;

namespace POS_System.Business.Services.Interfaces
{
    public interface ITaxService
    {
        public Task<IEnumerable<TaxDto?>> GetAllTaxesAsync(CancellationToken cancellationToken);
        public Task CreateTaxAsync(TaxDto? taxDto, CancellationToken cancellationToken);
        public Task DeleteTaxAsync(int id, CancellationToken cancellationToken);
        public Task UpdateTaxAsync(int id, TaxDto? taxDto, CancellationToken cancellationToken);
        public Task<TaxDto?> GetTaxByIdAsync(int id, CancellationToken cancellationToken);
    }
}
