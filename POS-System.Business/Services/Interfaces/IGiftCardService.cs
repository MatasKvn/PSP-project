using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface IGiftCardService
    {
        Task<GiftCardResponse> CreateGiftCardAsync(GiftCardRequest GiftCardRequest, CancellationToken cancellationToken);
        Task<PagedResponse<GiftCardResponse>> GetAllGiftCardsAsync(CancellationToken cancellationToken, int pageNum, int pageSize);
        Task<GiftCardResponse?> GetGiftCardByIdAsync(string id, CancellationToken cancellationToken);
        Task<GiftCardResponse> UpdateGiftCardAsync(string id, GiftCardRequest giftCardUpdateRequestDto, CancellationToken cancellationToken);
        Task DeleteGiftCardAsync(string id, CancellationToken cancellationToken);
    }
}
