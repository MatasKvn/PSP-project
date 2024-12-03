using POS_System.Data.Database;
using POS_System.Data.Repositories.Base;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities.Generic;

namespace POS_System.Data.Repositories
{
    public class GenericManyToManyRepository<TLeft, TRight, TManyToMany>(ApplicationDbContext dbContext) : Repository<TManyToMany>(dbContext), IGenericManyToManyRepository<TLeft, TRight, TManyToMany> where TManyToMany : BaseManyToManyEntity<TLeft, TRight>
    {
    }
}
