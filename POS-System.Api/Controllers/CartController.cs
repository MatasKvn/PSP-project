using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [ApiController]
    [Route("api/carts")]
    public class CartController(ICartService _cartService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken, int pageNum = 0, int pageSize = 35)
        {
            var result = await _cartService.GetAllAsync(cancellationToken, pageNum, pageSize);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByID([FromRoute] int id, CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetByIdAsync(id, cancellationToken);
            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CartRequest cartDto, CancellationToken cancellationToken)
        {
            var cart = await _cartService.CreateCartAsync(cartDto, cancellationToken);
            return Ok(cart);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _cartService.DeleteCartAsync(id, cancellationToken);
            return Ok();
        }

        
        [HttpPatch("{id:int}/discount")]
        public async Task<IActionResult> ApplyDiscountToCart([FromRoute] int id, [FromBody] ApplyDiscountRequest discountRequest, CancellationToken cancellationToken)
        {
            var response = await _cartService.ApplyDiscountForCartAsync(id, discountRequest, cancellationToken);
            
            return Ok(response);
        }
    }
}
