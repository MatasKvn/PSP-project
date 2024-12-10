using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface ICartItemService
    {
        public Task<CartItemResponse> CreateCartItemAsync(CartItemRequest CartItemRequest, CancellationToken cancellationToken);
        public Task<PagedResponse<CartItemResponse>> GetAllCartItemsAsync(int cartId, CancellationToken cancellationToken, int pageNum, int pageSize);
        public Task<CartItemResponse?> GetCartItemByIdAndCartIdAsync(int cartId, int id, CancellationToken cancellationToken);
        public Task<CartItemResponse> UpdateCartItemAsync(int cartId, int id, CartItemRequest cartItemUpdateRequestDto, CancellationToken cancellationToken);
        public Task DeleteCartItemAsync(int cartId, int id, CancellationToken cancellationToken);
        public Task LinkCartItemToProductModificationsAsync(int cartItemId, int[] productModificationIdList, CancellationToken cancellationToken);
        public Task UnlinkCartItemFromProductModificationsAsync(int cartItemId, int[] productModificationIdList, CancellationToken cancellationToken);
    }
}
