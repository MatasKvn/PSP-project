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

    public async Task<GiftCardResponse?> GetGiftCardByIdAsync(int id, CancellationToken cancellationToken)
    {
        IdValidator.ValidateId(id);

        var giftCard = await unitOfWork.GiftCardRepository.GetByExpressionAsync(giftcard => giftcard.Id == id && giftcard.Date >= DateOnly.FromDateTime(DateTime.UtcNow), cancellationToken)
            ?? throw new NotFoundException($"Gift card with id {id} does not exist.");

        return mapper.Map<GiftCardResponse>(giftCard);
    }

    public async Task<GiftCardResponse> CreateGiftCardAsync(GiftCardRequest GiftCardRequest, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(GiftCardRequest, nameof(GiftCardRequest));

        var existingGiftCard = await unitOfWork.GiftCardRepository.GetByExpressionAsync(g => g.Code == GiftCardRequest.Code, cancellationToken);
        if (existingGiftCard is not null)
        {
            throw new BadRequestException("Gift card with this code already exist.", nameof(existingGiftCard.Code));
        }

        var newGiftCard = mapper.Map<GiftCard>(GiftCardRequest);

        await unitOfWork.GiftCardRepository.CreateAsync(newGiftCard, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<GiftCardResponse>(newGiftCard);
    }

    public async Task<GiftCardResponse> UpdateGiftCardAsync(int id, GiftCardRequest GiftCardRequest, CancellationToken cancellationToken)
    {
        IdValidator.ValidateId(id);
        ArgumentNullException.ThrowIfNull(GiftCardRequest, nameof(GiftCardRequest));

        var giftCardToUpdate = await unitOfWork.GiftCardRepository.GetByIdAsync(id, cancellationToken);
        if (giftCardToUpdate is null)
        {
            throw new NotFoundException($"Gift card with id {id} does not exist.", nameof(giftCardToUpdate));
        }

        mapper.Map(GiftCardRequest, giftCardToUpdate);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<GiftCardResponse>(giftCardToUpdate);
    }

    public async Task DeleteGiftCardAsync(int id, CancellationToken cancellationToken)
    {
        IdValidator.ValidateId(id);

        var giftCardToDelete = await unitOfWork.GiftCardRepository.GetByIdAsync(id, cancellationToken);
        if (giftCardToDelete is null)
        {
            throw new NotFoundException($"Gift card with id {id} does not exist.", nameof(giftCardToDelete));
        }

        unitOfWork.GiftCardRepository.Delete(giftCardToDelete);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}