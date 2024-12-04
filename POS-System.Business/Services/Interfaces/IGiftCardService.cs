using POS_System.Business.Dtos;
using POS_System.Business.Dtos.GiftCard;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface IGiftCardService
    {
        Task<GiftCardResponseDto> CreateGiftCardAsync(GiftCardRequestDto giftCardRequestDto, CancellationToken cancellationToken);
        Task<PagedResponse<GiftCardResponseDto>> GetAllGiftCardsAsync(CancellationToken cancellationToken, int pageNum, int pageSize);
        Task<GiftCardResponseDto?> GetGiftCardByIdAsync(int id, CancellationToken cancellationToken);
        Task<GiftCardResponseDto> UpdateGiftCardAsync(int id, GiftCardRequestDto giftCardUpdateRequestDto, CancellationToken cancellationToken);
        Task DeleteGiftCardAsync(int id, CancellationToken cancellationToken);
    }
}
