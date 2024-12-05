using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface ICartItemService
    {
        Task<CartItemResponse> CreateCartItemAsync(CartItemRequest CartItemRequest, CancellationToken cancellationToken);
        Task<PagedResponse<CartItemResponse>> GetAllCartItemsAsync(int cartId, CancellationToken cancellationToken, int pageNum, int pageSize);
        Task<CartItemResponse?> GetCartItemByIdAndCartIdAsync(int cartId, int id, CancellationToken cancellationToken);
        Task<CartItemResponse> UpdateCartItemAsync(int cartId, int id, CartItemRequest cartItemUpdateRequestDto, CancellationToken cancellationToken);
        Task DeleteCartItemAsync(int cartId, int id, CancellationToken cancellationToken);
    }
}
