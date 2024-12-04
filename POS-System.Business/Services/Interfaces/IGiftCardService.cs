using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface IGiftCardService
    {
        Task<GiftCardResponse> CreateGiftCardAsync(GiftCardRequest GiftCardRequest, CancellationToken cancellationToken);
        Task<PagedResponse<GiftCardResponse>> GetAllGiftCardsAsync(CancellationToken cancellationToken, int pageNum, int pageSize);
        Task<GiftCardResponse?> GetGiftCardByIdAsync(int id, CancellationToken cancellationToken);
        Task<GiftCardResponse> UpdateGiftCardAsync(int id, GiftCardRequest giftCardUpdateRequestDto, CancellationToken cancellationToken);
        Task DeleteGiftCardAsync(int id, CancellationToken cancellationToken);
    }
}
