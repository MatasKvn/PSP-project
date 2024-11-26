using POS_System.Data.Database;
using POS_System.Data.Repositories.Base;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Data.Repositories
{
    public class ProductOnTaxRepository(ApplicationDbContext<int> dbContext) : Repository<ProductOnTax>(dbContext), IProductOnTaxRepository
    {
    }
}
