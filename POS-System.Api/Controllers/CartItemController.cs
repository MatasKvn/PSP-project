using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using POS_System.Business.Dtos.Request;

namespace POS_System.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/carts/{cartid:int}/items")]

public class CartItemController(ICartItemService cartItemService) : ControllerBase
{
    [Authorize(Policy = "CartItemRead")]
    [HttpGet]
    public async Task<IActionResult> GetAllCartItems([FromRoute] int cartid, CancellationToken cancellationToken, int pageNum = 0, int pageSize = 10)
    {
        Console.WriteLine($"cartid: {cartid}");
        var cartItems = await cartItemService.GetAllCartItemsAsync(cartid, cancellationToken, pageNum, pageSize);

        return Ok(cartItems);
    }

    [HttpGet("{id:int}")]
    [Authorize(Policy = "CartItemRead")]
    public async Task<IActionResult> GetCartItemByIdAndCartId([FromRoute] int cartid, [FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await cartItemService.GetCartItemByIdAndCartIdAsync(cartid, id, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = "CartItemWrite")]
    public async Task<IActionResult> CreateCartItem([FromBody] CartItemRequest CartItemRequest, CancellationToken cancellationToken)
    {
        var newcartItem = await cartItemService.CreateCartItemAsync(CartItemRequest, cancellationToken);
        return Ok(newcartItem);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "CartItemWrite")]
    public async Task<IActionResult> UpdateCartItem([FromRoute] int cartid, [FromRoute] int id, [FromBody] CartItemRequest cartItemUpdateRequestDto, CancellationToken cancellationToken)
    {
        var updatedcartItem = await cartItemService.UpdateCartItemAsync(cartid, id, cartItemUpdateRequestDto, cancellationToken);

        return Ok(updatedcartItem);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "CartItemWrite")]
    public async Task<IActionResult> DeleteCartItem([FromRoute] int cartid, [FromRoute] int id, CancellationToken cancellationToken)
    {
        await cartItemService.DeleteCartItemAsync(cartid, id, cancellationToken);

        return NoContent();
    }
}