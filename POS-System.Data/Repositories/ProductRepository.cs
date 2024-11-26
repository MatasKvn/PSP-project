using POS_System.Data.Repositories.Base;
using POS_System.Data.Database;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace POS_System.Data.Repositories;

public class ProductRepository(ApplicationDbContext dbContext) : Repository<Product>(dbContext), IProductRepository
{
    public async Task<int> GetMaxProductIdAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.MaxAsync(p => p.ProductId, cancellationToken);
    }
}