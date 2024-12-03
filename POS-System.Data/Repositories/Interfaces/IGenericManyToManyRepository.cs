using POS_System.Data.Repositories.Base;
using POS_System.Domain.Entities.Generic;

namespace POS_System.Data.Repositories.Interfaces
{
    public interface IGenericManyToManyRepository<TLeft, TRight, TManyToMany> : IRepository<TManyToMany> where TManyToMany : BaseManyToManyEntity<TLeft, TRight>
    {
    }
}
