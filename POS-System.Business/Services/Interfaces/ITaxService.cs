using POS_System.Business.Dtos;

namespace POS_System.Business.Services.Interfaces
{
    public interface ITaxService
    {
        public Task<IEnumerable<TaxDto?>> GetAllTaxesAsync(CancellationToken cancellationToken);
    }
}
