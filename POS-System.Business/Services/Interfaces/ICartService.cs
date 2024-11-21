using POS_System.Business.Dtos;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartDto>> GetAllAsync(CancellationToken cancellationToken);

        Task<CartDto> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task CreateCartAsync(CartDto cartDto, CancellationToken cancellationToken);

        Task DeleteCartAsync(int id, CancellationToken cancellationToken);
    }
}
