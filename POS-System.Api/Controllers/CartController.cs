using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [ApiController]
    [Route("api/carts")]
    public class CartController(ICartService _cartService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _cartService.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByID(int id, CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetByIdAsync(id, cancellationToken);
            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CartDto cartDto, CancellationToken cancellationToken)
        {
            await _cartService.CreateCartAsync(cartDto, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _cartService.DeleteCartAsync(id, cancellationToken);
            return Ok();
        }
    }
}
