using POS_System.Business.Dtos;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<GetCartDto>> GetAllAsync(CancellationToken cancellationToken);

        Task<GetCartDto> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task CreateCartAsync(CreateCartDto cartDto, CancellationToken cancellationToken);

        Task DeleteCartAsync(int id, CancellationToken cancellationToken);
    }
}
