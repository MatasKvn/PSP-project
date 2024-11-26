using POS_System.Data.Repositories.Base;
using POS_System.Domain.Entities;

namespace POS_System.Data.Repositories.Interfaces;

public interface IProductModificationRepository : IRepository<ProductModification>
{
    public Task<int> GetMaxProductModificationIdAsync(CancellationToken cancellationToken);
}