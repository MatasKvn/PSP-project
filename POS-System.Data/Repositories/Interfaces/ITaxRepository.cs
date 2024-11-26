using POS_System.Data.Repositories.Base;
using POS_System.Domain.Entities;

namespace POS_System.Data.Repositories.Interfaces;

public interface ITaxRepository : IRepository<Tax>
{
    public Task<int> GetMaxTaxIdAsync(CancellationToken cancellationToken);
}