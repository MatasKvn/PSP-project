using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;
using POS_System.Common.Enums;

namespace POS_System.Business.Services.Interfaces
{
    public interface ICartService
    {
        Task<PagedResponse<CartResponse>> GetAllAsync(CancellationToken cancellationToken, int pageNum, int pageSize);
        
        Task<CartResponse> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<CartResponse> CreateCartAsync(CartRequest cartDto, CancellationToken cancellationToken);

        Task DeleteCartAsync(int id, CancellationToken cancellationToken);

        Task UpdateCartStatusAsync(int id, CartStatusEnum status, CancellationToken cancellationToken = default);

        Task<CartDiscountResponse> ApplyDiscountForCartAsync(int id, ApplyDiscountRequest discountRequest, CancellationToken cancellationToken);
    
        Task<CartDiscountResponse?> GetCartDiscountAsync(int id, CancellationToken cancellationToken);
    }
}
