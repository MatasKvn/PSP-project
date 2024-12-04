using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface ICartDiscountService
    {
        public Task<IEnumerable<CartDiscountResponse>> GetAllCartDiscountsAsync(CancellationToken cancellationToken);
        public Task<CartDiscountResponse> CreateCartDiscountAsync(CartDiscountRequest cartDiscountDto, CancellationToken cancellationToken);
        public Task DeleteCartDiscountAsync(int id, CancellationToken cancellationToken);
        public Task<CartDiscountResponse> UpdateCartDiscountAsync(int id, CartDiscountRequest cartDiscountDto, CancellationToken cancellationToken);
        public Task<CartDiscountResponse> GetCartDiscountByIdAsync(int id, CancellationToken cancellationToken);
        public Task LinkCartDiscountToCartsAsync(int cartDiscountId, int[] cartIdList, CancellationToken cancellationToken);
        public Task UnlinkCartDiscountFromItemsAsync(int cartDiscountId, int[] cartIdList, CancellationToken cancellationToken);
        public Task<IEnumerable<CartDiscountResponse>> GetCartDiscountsLinkedToCartId(int cartId, DateTime? timeStamp, CancellationToken cancellationToken);

    }
}
