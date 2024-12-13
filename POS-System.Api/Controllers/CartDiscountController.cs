using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [Route("api/cart-discount")]
    [ApiController]
    public class CartDiscountController(ICartDiscountService _cartDiscountService) : ControllerBase
    {
        // [HttpGet]
        // public async Task<IActionResult> GetAllCartDiscounts(CancellationToken cancellationToken)
        // {
        //     var cartDiscounts = await _cartDiscountService.GetAllCartDiscountsAsync(cancellationToken);
        //     return Ok(cartDiscounts);
        // }

        [HttpPost]
        public async Task<IActionResult> CreateCartDiscount([FromBody] CartDiscountRequest cartDiscountDto, CancellationToken cancellationToken)
        {
            var createdCartDiscount = await _cartDiscountService.CreateCartDiscountAsync(cartDiscountDto, cancellationToken);
            return Ok(createdCartDiscount);
        }

        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetCartDiscountById(int id, CancellationToken cancellationToken)
        // {
        //     var cartDiscount = await _cartDiscountService.GetCartDiscountByIdAsync(id, cancellationToken);
        //     return Ok(cartDiscount);
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteCartDiscountById(int id, CancellationToken cancellationToken)
        // {
        //     await _cartDiscountService.DeleteCartDiscountAsync(id, cancellationToken);
        //     return Ok();
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateCartDiscountById(int id, [FromBody] CartDiscountRequest cartDiscountDto, CancellationToken cancellationToken)
        // {
        //     var updatedCartDiscount = await _cartDiscountService.UpdateCartDiscountAsync(id, cartDiscountDto, cancellationToken);
        //     return Ok(updatedCartDiscount);
        // }

        // [HttpPut("{id}/link")]
        // public async Task<IActionResult> LinkCartDiscountToCarts(int id, [FromBody] int[] cartIdList, CancellationToken cancellationToken)
        // {
        //     await _cartDiscountService.LinkCartDiscountToCartsAsync(id, cartIdList, cancellationToken);

        //     return Ok();
        // }

        // [HttpPut("{id}/unlink")]
        // public async Task<IActionResult> UnlinkCartDiscountFromCarts(int id, [FromBody] int[] cartIdList, CancellationToken cancellationToken)
        // {
        //     await _cartDiscountService.UnlinkCartDiscountFromItemsAsync(id, cartIdList, cancellationToken);

        //     return Ok();
        // }

        // //Leave timeStamp null if you want to get only the active carts
        // [HttpGet("cart/{id}")]
        // public async Task<IActionResult> GetCartDiscountsLinkedToCartId(int id, [FromQuery] DateTime? timeStamp, CancellationToken cancellationToken)
        // {
        //     var cartDiscounts = await _cartDiscountService.GetCartDiscountsLinkedToCartId(id, timeStamp, cancellationToken);
        //     return Ok(cartDiscounts);
        // }
    }
}
