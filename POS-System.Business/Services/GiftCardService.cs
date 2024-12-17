using AutoMapper;
using POS_System.Business.Validators;
using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;
using POS_System.Common.Exceptions;
using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services;

public class GiftCardService(IUnitOfWork unitOfWork, IMapper mapper) : IGiftCardService
{
    public async Task<PagedResponse<GiftCardResponse>> GetAllGiftCardsAsync(CancellationToken cancellationToken, int pageNum, int pageSize)
    {
        var (giftCards, totalCount) = await unitOfWork.GiftCardRepository.GetAllByExpressionWithPaginationAsync(giftcard => giftcard.Date >= DateOnly.FromDateTime(DateTime.UtcNow),
            pageSize,
            pageNum,
            cancellationToken
        );

        var mappedGiftCards = mapper.Map<IEnumerable<GiftCardResponse>>(giftCards);
        return new PagedResponse<GiftCardResponse>(totalCount, pageSize, pageNum, mappedGiftCards);
    }

    public async Task<GiftCardResponse?> GetGiftCardByIdAsync(string id, CancellationToken cancellationToken)
    {
        var giftCard = await unitOfWork.GiftCardRepository.GetByExpressionAsync(giftcard => giftcard.Id == id && giftcard.Date >= DateOnly.FromDateTime(DateTime.UtcNow), cancellationToken)
            ?? throw new NotFoundException($"Gift card with id {id} does not exist.");

        return mapper.Map<GiftCardResponse>(giftCard);
    }

    public async Task<GiftCardResponse> CreateGiftCardAsync(GiftCardRequest GiftCardRequest, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(GiftCardRequest, nameof(GiftCardRequest));

        var newGiftCard = mapper.Map<GiftCard>(GiftCardRequest);

        var rnd = new Random();
        do
        {
            newGiftCard.Id = rnd.Next(10000000, 99999999).ToString();
        } while ((await unitOfWork.GiftCardRepository.GetByIdStringAsync(newGiftCard.Id, cancellationToken)) is not null);

        await unitOfWork.GiftCardRepository.CreateAsync(newGiftCard, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<GiftCardResponse>(newGiftCard);
    }

    public async Task<GiftCardResponse> UpdateGiftCardAsync(string id, GiftCardRequest GiftCardRequest, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(GiftCardRequest, nameof(GiftCardRequest));

        var giftCardToUpdate = await unitOfWork.GiftCardRepository.GetByIdStringAsync(id, cancellationToken);
        if (giftCardToUpdate is null)
        {
            throw new NotFoundException($"Gift card with id {id} does not exist.", nameof(giftCardToUpdate));
        }

        mapper.Map(GiftCardRequest, giftCardToUpdate);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<GiftCardResponse>(giftCardToUpdate);
    }

    public async Task DeleteGiftCardAsync(string id, CancellationToken cancellationToken)
    {
        var giftCardToDelete = await unitOfWork.GiftCardRepository.GetByIdStringAsync(id, cancellationToken);
        if (giftCardToDelete is null)
        {
            throw new NotFoundException($"Gift card with id {id} does not exist.", nameof(giftCardToDelete));
        }

        unitOfWork.GiftCardRepository.Delete(giftCardToDelete);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}