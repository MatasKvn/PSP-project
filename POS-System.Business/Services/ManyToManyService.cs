using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Base;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities.Generic;
using POS_System.Domain.Entities.Interfaces;
using System.Threading;

namespace POS_System.Business.Services
{
    public class ManyToManyService<TLeft, TRight, TManyToMany>(IUnitOfWork _unitOfWork) : IManyToManyService<TLeft, TRight, TManyToMany>
        where TLeft : class, ILinkable
        where TRight : class, ILinkable
        where TManyToMany : BaseManyToManyEntity<TLeft, TRight>, new()
    {
        public async Task MarkActiveLinksDeletedAsync(IRepository<TManyToMany> linkRepository, int id, bool markingLeft, CancellationToken cancellationToken)
        {
            IEnumerable<TManyToMany> itemLinks = markingLeft ?
                await linkRepository.GetAllByExpressionAsync(x => x.LeftEntityId == id && x.EndDate == null, cancellationToken)
                : await linkRepository.GetAllByExpressionAsync(x => x.RightEntityId == id && x.EndDate == null, cancellationToken);

            foreach (var itemLink in itemLinks)
            {
                itemLink.EndDate = DateTime.UtcNow;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task RelinkItemToItem(IRepository<TManyToMany> linkRepository, int oldItemId, int newItemId, bool relinkingLeft, CancellationToken cancellationToken)
        {
            IEnumerable<TManyToMany> itemLinks = relinkingLeft ?
                await linkRepository.GetAllByExpressionAsync(x => x.LeftEntityId == oldItemId && x.EndDate == null, cancellationToken)
                : await linkRepository.GetAllByExpressionAsync(x => x.RightEntityId == oldItemId && x.EndDate == null, cancellationToken);

            foreach (var itemLink in itemLinks)
            {
                itemLink.EndDate = DateTime.UtcNow;

                TManyToMany newItemLink;
                if (relinkingLeft)
                {
                    newItemLink = new TManyToMany
                    {
                        LeftEntityId = newItemId,
                        RightEntityId = itemLink.RightEntityId,
                        StartDate = DateTime.UtcNow,
                        EndDate = null
                    };
                }
                else
                {
                    newItemLink = new TManyToMany
                    {
                        LeftEntityId = itemLink.LeftEntityId,
                        RightEntityId = newItemId,
                        StartDate = DateTime.UtcNow,
                        EndDate = null
                    };
                }
                await linkRepository.CreateAsync(newItemLink, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task LinkItemToItemsAsync(IRepository<TLeft> leftRepository, IRepository<TRight> rightRepository, IRepository<TManyToMany> linksRepository,
            int itemId, int[] linkingItemsIdList, bool linkableItemIsLeft, CancellationToken cancellationToken)
        {
            ILinkable? linkableItem = linkableItemIsLeft ?
                await leftRepository.GetByIdAsync(itemId, cancellationToken)
                : await rightRepository.GetByIdAsync(itemId, cancellationToken);

            if (linkableItem is not null && linkableItem.IsDeleted == false)
            {
                foreach (var linkItemId in linkingItemsIdList)
                {
                    ILinkable? linkingItem = linkableItemIsLeft ?
                        await rightRepository.GetByIdAsync(linkItemId, cancellationToken)
                        : await leftRepository.GetByIdAsync(linkItemId, cancellationToken);

                    if (linkingItem is not null && linkingItem.IsDeleted == false)
                    {
                        TManyToMany? existingLink = linkableItemIsLeft ?
                            await linksRepository.GetByExpressionWithIncludesAsync(x => x.LeftEntityId == itemId && x.RightEntityId == linkItemId && x.EndDate == null)
                            : await linksRepository.GetByExpressionWithIncludesAsync(x => x.LeftEntityId == linkItemId && x.RightEntityId == itemId && x.EndDate == null);

                        if (existingLink is null)
                        {
                            TManyToMany newItemLink;
                            if (linkableItemIsLeft)
                            {
                                newItemLink = new TManyToMany
                                {
                                    LeftEntityId = itemId,
                                    RightEntityId = linkItemId,
                                    StartDate = DateTime.UtcNow,
                                    EndDate = null
                                };
                            }
                            else
                            {
                                newItemLink = new TManyToMany
                                {
                                    LeftEntityId = linkItemId,
                                    RightEntityId = itemId,
                                    StartDate = DateTime.UtcNow,
                                    EndDate = null
                                };
                            }
                            await linksRepository.CreateAsync(newItemLink, cancellationToken);
                        }
                    }
                }
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UnlinkItemFromItemsAsync(IRepository<TLeft> leftRepository, IRepository<TRight> rightRepository, IRepository<TManyToMany> linksRepository,
            int itemId, int[] linkingItemsIdList, bool linkableItemIsLeft, CancellationToken cancellationToken)
        {
            ILinkable? linkableItem = linkableItemIsLeft ?
                await leftRepository.GetByIdAsync(itemId, cancellationToken)
                : await rightRepository.GetByIdAsync(itemId, cancellationToken);

            if (linkableItem is not null && linkableItem.IsDeleted == false)
            {
                foreach (var linkItemId in linkingItemsIdList)
                {
                    ILinkable? linkingItem = linkableItemIsLeft ?
                        await rightRepository.GetByIdAsync(linkItemId, cancellationToken)
                        : await leftRepository.GetByIdAsync(linkItemId, cancellationToken);

                    if (linkingItem is not null && linkingItem.IsDeleted == false)
                    {
                        TManyToMany? existingLink = linkableItemIsLeft ?
                            await linksRepository.GetByExpressionWithIncludesAsync(x => x.LeftEntityId == itemId && x.RightEntityId == linkItemId && x.EndDate == null)
                            : await linksRepository.GetByExpressionWithIncludesAsync(x => x.LeftEntityId == linkItemId && x.RightEntityId == itemId && x.EndDate == null);

                        if (existingLink is not null)
                        {
                            existingLink.EndDate = DateTime.UtcNow;
                        }
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<int>> GetActiveLinkIds(IRepository<TManyToMany> linkRepository, int id, bool forLeft, CancellationToken cancellationToken)
        {
            IEnumerable<TManyToMany> itemLinks = forLeft ?
                await linkRepository.GetAllByExpressionAsync(x => x.LeftEntityId == id && x.EndDate == null, cancellationToken)
                : await linkRepository.GetAllByExpressionAsync(x => x.RightEntityId == id && x.EndDate == null, cancellationToken);

            IEnumerable<int> linkedItemIds = forLeft ?
                itemLinks.Select(x => x.RightEntityId).ToList()
                : itemLinks.Select(x => x.LeftEntityId).ToList();

            return linkedItemIds;
        }

        //Leave queryDate null if you only want active link ids
        public async Task<IEnumerable<int>> GetLinkIdsAsync(IRepository<TManyToMany> linkRepository, int id, bool forLeft, DateTime? queryDate, CancellationToken cancellationToken)
        {
            IEnumerable<TManyToMany> itemLinks;

            if (queryDate is null)
            {
                itemLinks = forLeft ?
                    await linkRepository.GetAllByExpressionAsync(x => x.LeftEntityId == id && x.EndDate == null, cancellationToken)
                    : await linkRepository.GetAllByExpressionAsync(x => x.RightEntityId == id && x.EndDate == null, cancellationToken);
            }
            else
            {
                itemLinks = forLeft ?
                    await linkRepository.GetAllByExpressionAsync(x => x.LeftEntityId == id && queryDate >= x.StartDate && (queryDate <= x.EndDate || x.EndDate == null), cancellationToken)
                    : await linkRepository.GetAllByExpressionAsync(x => x.RightEntityId == id && queryDate >= x.StartDate && (queryDate <= x.EndDate || x.EndDate == null), cancellationToken);
            }

            IEnumerable<int> linkedItemIds = forLeft ?
                itemLinks.Select(x => x.RightEntityId).ToList()
                : itemLinks.Select(x => x.LeftEntityId).ToList();

            return linkedItemIds;
        }
    }
}
