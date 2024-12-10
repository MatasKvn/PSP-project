using POS_System.Data.Repositories.Base;
using POS_System.Domain.Entities.Generic;
using POS_System.Domain.Entities.Interfaces;

namespace POS_System.Business.Services.Interfaces
{
    public interface IManyToManyService<TLeft, TRight, TManyToMany>
        where TLeft : class, ILinkable
        where TRight : class, ILinkable
        where TManyToMany : BaseManyToManyEntity<TLeft, TRight>
    {
        public Task MarkActiveLinksDeletedAsync(IRepository<TManyToMany> linkRepository, int id, bool markingLeft, CancellationToken cancellationToken);
        public Task RelinkItemToItemAsync(IRepository<TManyToMany> linkRepository, int oldItemId, int newItemId, bool relinkingLeft, CancellationToken cancellationToken);
        public Task LinkItemToItemsAsync(IRepository<TLeft> leftRepository, IRepository<TRight> rightRepository, IRepository<TManyToMany> linksRepository,
            int itemId, int[] linkingItemsIdList, bool linkableItemIsLeft, CancellationToken cancellationToken);
        public Task UnlinkItemFromItemsAsync(IRepository<TLeft> leftRepository, IRepository<TRight> rightRepository, IRepository<TManyToMany> linksRepository,
            int itemId, int[] linkingItemsIdList, bool linkableItemIsLeft, CancellationToken cancellationToken);
        public Task<IEnumerable<int>> GetLinkIdsAsync(IRepository<TManyToMany> linkRepository, int id, bool forLeft, DateTime? queryDate, CancellationToken cancellationToken);
    }
}
