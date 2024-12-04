using AutoMapper;
using POS_System.Business.Validators;
using POS_System.Business.Dtos.GiftCard;
using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;
using POS_System.Common.Exceptions;
using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services;

public class GiftCardService(IUnitOfWork unitOfWork, IMapper mapper) : IGiftCardService
{
    public async Task<PagedResponse<GiftCardResponseDto>> GetAllGiftCardsAsync(CancellationToken cancellationToken, int pageNum, int pageSize)
    {
        var (giftCards, totalCount) = await unitOfWork.GiftCardRepository.GetAllByExpressionWithPaginationAsync(giftcard => giftcard.Date >= DateOnly.FromDateTime(DateTime.Now),
            pageSize,
            pageNum,
            cancellationToken
        );

        var mappedGiftCards = mapper.Map<IEnumerable<GiftCardResponseDto>>(giftCards);
        return new PagedResponse<GiftCardResponseDto>(totalCount, pageSize, pageNum, mappedGiftCards);
    }

    public async Task<GiftCardResponseDto?> GetGiftCardByIdAsync(int id, CancellationToken cancellationToken)
    {
        IdValidator.ValidateId(id);

        var giftCard = await unitOfWork.GiftCardRepository.GetByExpressionAsync(giftcard => giftcard.Id == id && giftcard.Date >= DateOnly.FromDateTime(DateTime.Now), cancellationToken)
            ?? throw new NotFoundException($"Gift card with id {id} does not exist.");

        return mapper.Map<GiftCardResponseDto>(giftCard);
    }

    public async Task<GiftCardResponseDto> CreateGiftCardAsync(GiftCardRequestDto giftCardRequestDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(giftCardRequestDto, nameof(giftCardRequestDto));

        var existingGiftCard = await unitOfWork.GiftCardRepository.GetByExpressionAsync(g => g.Code == giftCardRequestDto.Code, cancellationToken);
        if (existingGiftCard is not null)
        {
            throw new BadRequestException("Gift card with this code already exist.", nameof(existingGiftCard.Code));
        }

        var newGiftCard = mapper.Map<GiftCard>(giftCardRequestDto);

        await unitOfWork.GiftCardRepository.CreateAsync(newGiftCard, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<GiftCardResponseDto>(newGiftCard);
    }

    public async Task<GiftCardResponseDto> UpdateGiftCardAsync(int id, GiftCardRequestDto giftCardRequestDto, CancellationToken cancellationToken)
    {
        IdValidator.ValidateId(id);
        ArgumentNullException.ThrowIfNull(giftCardRequestDto, nameof(giftCardRequestDto));

        var giftCardToUpdate = await unitOfWork.GiftCardRepository.GetByIdAsync(id, cancellationToken);
        if (giftCardToUpdate is null)
        {
            throw new NotFoundException($"Gift card with id {id} does not exist.", nameof(giftCardToUpdate));
        }

        mapper.Map(giftCardRequestDto, giftCardToUpdate);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<GiftCardResponseDto>(giftCardToUpdate);
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