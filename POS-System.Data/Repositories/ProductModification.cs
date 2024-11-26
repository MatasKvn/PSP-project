using POS_System.Data.Repositories.Base;
using POS_System.Data.Database;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace POS_System.Data.Repositories;

public class ProductModificationRepository(ApplicationDbContext<int> dbContext) : Repository<ProductModification>(dbContext), IProductModificationRepository
{
    public async Task<int> GetMaxProductModificationIdAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.MaxAsync(p => p.ProductModificationId, cancellationToken);
    }
}