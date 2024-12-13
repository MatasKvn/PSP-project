using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [Route("/api/payments")]
    [ApiController]
    public class PaymentController(IPaymentService paymentService) : ControllerBase
    {
        [HttpPost("/full-checkout")]
        public async Task<IActionResult> FullCheckoutAsync([FromBody] CheckoutRequest checkoutRequest, CancellationToken token = default)
        {
            var response = await paymentService.FullCheckoutAsync(checkoutRequest, Request.Headers.Referer, token);

            return Ok(response);
        }

        [HttpPost("/init-partial-checkout")]
        public async Task<IActionResult> InitializePartialCheckoutAsync([FromBody] InitPartialCheckoutRequest checkoutRequest, CancellationToken token = default)
        {
            var response = await paymentService.InitializePartialCheckoutAsync(checkoutRequest, token);

            return Ok(response);
        }

        [HttpPost("/partial-checkout")]
        public async Task<IActionResult> PartialCheckoutAsync([FromBody] PartialCheckoutRequest checkoutRequest, CancellationToken token = default)
        {
            var response = await paymentService.PartialCheckoutAsync(checkoutRequest, token);

            return Ok(response);
        }

        [HttpGet("/full-checkout-success")]
        public async Task<IActionResult> FullCheckoutSuccessAsync([FromQuery] DateTime transactionDate, [FromQuery] int cartId, [FromQuery] string sessionId)
        {
            var path = await paymentService.FullCheckoutSuccessAsync(transactionDate, sessionId, cartId);

            return Redirect(path);
        }

        [HttpGet("/partial-checkout-success")]
        public async Task<IActionResult> PartialCheckoutSuccessAsync([FromQuery] DateTime transactionDate, [FromQuery] int cartId, [FromQuery] string sessionId)
        {
            var path = await paymentService.PartialCheckoutSuccessAsync(transactionDate, sessionId, cartId);

            return Redirect(path);
        }
    }
}