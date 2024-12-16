using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/giftcards")]
public class GiftCardController(IGiftCardService giftCardService) : ControllerBase
{
    [Authorize(Policy = "GiftCardRead")]
    [HttpGet]
    public async Task<IActionResult> GetAllGiftCards(CancellationToken cancellationToken, int pageNum = 0, int pageSize = 10)
    {
        var giftCards = await giftCardService.GetAllGiftCardsAsync(cancellationToken, pageNum, pageSize);

        return Ok(giftCards);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "GiftCardRead")]
    public async Task<IActionResult> GetGiftCardById([FromRoute] string id, CancellationToken cancellationToken)
    {
        var result = await giftCardService.GetGiftCardByIdAsync(id, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = "GiftCardWrite")]
    public async Task<IActionResult> CreateGiftCard([FromBody] GiftCardRequest GiftCardRequest, CancellationToken cancellationToken)
    {
        var newGiftCard = await giftCardService.CreateGiftCardAsync(GiftCardRequest, cancellationToken);
        return Ok(newGiftCard);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "GiftCardWrite")]
    public async Task<IActionResult> UpdateGiftCard([FromRoute] string id, [FromBody] GiftCardRequest giftCardUpdateRequestDto, CancellationToken cancellationToken)
    {
        var updatedGiftCard = await giftCardService.UpdateGiftCardAsync(id, giftCardUpdateRequestDto, cancellationToken);

        return Ok(updatedGiftCard);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "GiftCardWrite")]
    public async Task<IActionResult> DeleteGiftCard([FromRoute] string id, CancellationToken cancellationToken)
    {
        await giftCardService.DeleteGiftCardAsync(id, cancellationToken);

        return NoContent();
    }
}