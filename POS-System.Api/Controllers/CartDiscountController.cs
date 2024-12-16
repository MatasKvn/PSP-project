using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [Route("api/cart-discount")]
    [ApiController]
    public class CartDiscountController(ICartDiscountService _cartDiscountService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateCartDiscount([FromBody] CartDiscountRequest cartDiscountDto, CancellationToken cancellationToken)
        {
            var createdCartDiscount = await _cartDiscountService.CreateCartDiscountAsync(cartDiscountDto, cancellationToken);
            return Ok(createdCartDiscount);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartDiscountById(string id, CancellationToken cancellationToken)
        {
            var cartDiscount = await _cartDiscountService.GetCartDiscountByIdAsync(id, cancellationToken);
            return Ok(cartDiscount);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartDiscountById(string id, CancellationToken cancellationToken)
        {
            await _cartDiscountService.DeleteCartDiscountAsync(id, cancellationToken);
            return Ok();
        }
    }
}
