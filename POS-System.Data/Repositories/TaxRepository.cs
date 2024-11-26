using POS_System.Data.Repositories.Base;
using POS_System.Data.Database;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace POS_System.Data.Repositories;

public class TaxRepository(ApplicationDbContext dbContext) : Repository<Tax>(dbContext), ITaxRepository
{
    public async Task<int> GetMaxTaxIdAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.MaxAsync(t => t.TaxId, cancellationToken);
    }
}