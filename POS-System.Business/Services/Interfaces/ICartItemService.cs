using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface ICartItemService
    {
        Task<CartItemResponseDto> CreateCartItemAsync(CartItemRequestDto cartItemRequestDto, CancellationToken cancellationToken);
        Task<PagedResponse<CartItemResponseDto>> GetAllCartItemsAsync(int cartId, CancellationToken cancellationToken, int pageNum, int pageSize);
        Task<CartItemResponseDto?> GetCartItemByIdAndCartIdAsync(int cartId, int id, CancellationToken cancellationToken);
        Task<CartItemResponseDto> UpdateCartItemAsync(int cartId, int id, CartItemRequestDto cartItemUpdateRequestDto, CancellationToken cancellationToken);
        Task DeleteCartItemAsync(int cartId, int id, CancellationToken cancellationToken);
    }
}
