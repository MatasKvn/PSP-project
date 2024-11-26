using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.GiftCard;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers;

[ApiController]
[Route("api/giftcards")]
public class GiftCardController(IGiftCardService giftCardService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllGiftCards(CancellationToken cancellationToken, int pageNum = 0, int pageSize = 10)
    {
        var giftCards = await giftCardService.GetAllGiftCardsAsync(cancellationToken, pageNum, pageSize);

        return Ok(giftCards);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetGiftCardById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await giftCardService.GetGiftCardByIdAsync(id, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGiftCard([FromBody] GiftCardRequestDto giftCardRequestDto, CancellationToken cancellationToken)
    {
        var newGiftCard = await giftCardService.CreateGiftCardAsync(giftCardRequestDto, cancellationToken);
        return Ok(newGiftCard);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateGiftCard([FromRoute] int id, [FromBody] GiftCardRequestDto GiftCardUpdateRequestDto, CancellationToken cancellationToken)
    {
        var updatedGiftCard = await giftCardService.UpdateGiftCardAsync(id, GiftCardUpdateRequestDto, cancellationToken);

        return Ok(updatedGiftCard);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGiftCard([FromRoute] int id, CancellationToken cancellationToken)
    {
        await giftCardService.DeleteGiftCardAsync(id, cancellationToken);

        return NoContent();
    }
}