using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface ICartDiscountService
    {
        public Task<CartDiscountResponse> CreateCartDiscountAsync(CartDiscountRequest cartDiscountDto, CancellationToken cancellationToken);
        public Task DeleteCartDiscountAsync(string id, CancellationToken cancellationToken);
        public Task<CartDiscountResponse> GetCartDiscountByIdAsync(string id, CancellationToken cancellationToken);
    }
}
