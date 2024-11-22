using POS_System.Business.Dtos;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartResponse>> GetAllAsync(CancellationToken cancellationToken);

        Task<CartResponse> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<CartResponse> CreateCartAsync(CartRequest cartDto, CancellationToken cancellationToken);

        Task DeleteCartAsync(int id, CancellationToken cancellationToken);
    }
}
