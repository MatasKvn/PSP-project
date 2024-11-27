using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Base;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities.Generic;
using POS_System.Domain.Entities.Interfaces;

namespace POS_System.Business.Services
{
    public class ManyToManyService<TLeft, TRight, TManyToMany>(IUnitOfWork _unitOfWork) : IManyToManyService<TLeft, TRight, TManyToMany>
        where TLeft : class, ILinkable
        where TRight : class, ILinkable
        where TManyToMany : BaseManyToManyEntity<TLeft, TRight>, new()
    {
        public async Task MarkActiveLinksDeletedAsync(IRepository<TManyToMany> linkRepository, int id, bool markingLeft, CancellationToken cancellationToken)
        {
            IEnumerable<TManyToMany> itemLinks;

            if (markingLeft)
                itemLinks = await linkRepository.GetAllByExpressionAsync(x => x.LeftEntityId == id && x.EndDate == null, cancellationToken);
            itemLinks = await linkRepository.GetAllByExpressionAsync(x => x.RightEntityId == id && x.EndDate == null, cancellationToken);

            foreach (var itemLink in itemLinks)
            {
                itemLink.EndDate = DateTime.UtcNow;
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RelinkItemToItem(IRepository<TManyToMany> linkRepository, int oldItemId, int newItemId, bool relinkingLeft, CancellationToken cancellationToken)
        {
            IEnumerable<TManyToMany> itemLinks;

            if (relinkingLeft)
                itemLinks = await linkRepository.GetAllByExpressionAsync(x => x.LeftEntityId == oldItemId && x.EndDate == null, cancellationToken);
            itemLinks = await linkRepository.GetAllByExpressionAsync(x => x.RightEntityId == oldItemId && x.EndDate == null, cancellationToken);

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

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task LinkItemToItemsAsync(IRepository<TLeft> leftRepository, IRepository<TRight> rightRepository, IRepository<TManyToMany> linksRepository,
            int itemId, int[] linkingItemsIdList, bool linkableItemIsLeft, CancellationToken cancellationToken)
        {
            ILinkable? linkableItem;

            if (linkableItemIsLeft)
                linkableItem = await leftRepository.GetByIdAsync(itemId, cancellationToken);
            linkableItem = await rightRepository.GetByIdAsync(itemId, cancellationToken);

            if (linkableItem is not null && linkableItem.IsDeleted == false)
            {
                foreach (var linkItemId in linkingItemsIdList)
                {
                    ILinkable? linkingItem;

                    if (linkableItemIsLeft)
                        linkingItem = await rightRepository.GetByIdAsync(linkItemId, cancellationToken);
                    linkingItem = await leftRepository.GetByIdAsync(linkItemId, cancellationToken);

                    if (linkingItem is not null && linkingItem.IsDeleted == false)
                    {
                        TManyToMany? existingLink;

                        if (linkableItemIsLeft)
                            existingLink = await linksRepository.GetByExpressionWithIncludesAsync(x => x.LeftEntityId == itemId && x.RightEntityId == linkItemId && x.EndDate == null);
                        existingLink = await linksRepository.GetByExpressionWithIncludesAsync(x => x.LeftEntityId == linkItemId && x.RightEntityId == itemId && x.EndDate == null);

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
            ILinkable? linkableItem;

            if (linkableItemIsLeft)
                linkableItem = await leftRepository.GetByIdAsync(itemId, cancellationToken);
            linkableItem = await rightRepository.GetByIdAsync(itemId, cancellationToken);

            if (linkableItem is not null && linkableItem.IsDeleted == false)
            {
                foreach (var linkItemId in linkingItemsIdList)
                {
                    ILinkable? linkingItem;

                    if (linkableItemIsLeft)
                        linkingItem = await rightRepository.GetByIdAsync(linkItemId, cancellationToken);
                    linkingItem = await leftRepository.GetByIdAsync(linkItemId, cancellationToken);

                    if (linkingItem is not null && linkingItem.IsDeleted == false)
                    {
                        TManyToMany? existingLink;

                        if (linkableItemIsLeft)
                            existingLink = await linksRepository.GetByExpressionWithIncludesAsync(x => x.LeftEntityId == itemId && x.RightEntityId == linkItemId && x.EndDate == null);
                        existingLink = await linksRepository.GetByExpressionWithIncludesAsync(x => x.LeftEntityId == linkItemId && x.RightEntityId == itemId && x.EndDate == null);

                        if (existingLink is not null)
                        {
                            existingLink.EndDate = DateTime.UtcNow;
                        }
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
