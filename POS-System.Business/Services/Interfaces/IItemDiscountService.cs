using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface IItemDiscountService
    {
        public Task<PagedResponse<ItemDiscountResponse>> GetAllItemDiscountsAsync(CancellationToken cancellationToken, int pageNum, int pageSize);
        public Task<ItemDiscountResponse> CreateItemDiscountAsync(ItemDiscountRequest itemDiscountDto, CancellationToken cancellationToken);
        public Task DeleteItemDiscountAsync(int id, CancellationToken cancellationToken);
        public Task<ItemDiscountResponse> UpdateItemDiscountAsync(int id, ItemDiscountRequest itemDiscountDto, CancellationToken cancellationToken);
        public Task<ItemDiscountResponse> GetItemDiscountByIdAsync(int id, CancellationToken cancellationToken);
        public Task LinkItemDiscountToItemsAsync(int itemDiscountId, bool itemsAreProducts, int[] itemIdList, CancellationToken cancellationToken);
        public Task UnlinkItemDiscountFromItemsAsync(int itemDiscountId, bool itemsAreProducts, int[] itemIdList, CancellationToken cancellationToken);
        public Task<IEnumerable<ItemDiscountResponse>> GetItemDiscountsLinkedToItemId(int itemId, bool isProduct, DateTime? timeStamp, CancellationToken cancellationToken);
    }
}
