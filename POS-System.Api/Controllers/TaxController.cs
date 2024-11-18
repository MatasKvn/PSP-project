using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [ApiController]
    [Route("api/ApiTax")]
    public class TaxController(ITaxService _taxService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ApiGetAllValidTaxes (CancellationToken cancellationToken)
        {
            var taxes = await _taxService.GetAllTaxesAsync(cancellationToken);
            return Ok(taxes);
        }
    }
}