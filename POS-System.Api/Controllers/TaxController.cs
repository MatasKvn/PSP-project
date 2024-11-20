using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [ApiController]
    [Route("api/tax")]
    public class TaxController(ITaxService _taxService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTaxes (CancellationToken cancellationToken)
        {
            var taxes = await _taxService.GetAllTaxesAsync(cancellationToken);
            return Ok(taxes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTax([FromBody] TaxDto? taxDto, CancellationToken cancellationToken)
        {
            await _taxService.CreateTaxAsync(taxDto, cancellationToken);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaxById(int id, CancellationToken cancellationToken)
        {
            var tax = await _taxService.GetTaxByIdAsync(id, cancellationToken);
            return Ok(tax);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaxById(int id, CancellationToken cancellationToken)
        {
            await _taxService.DeleteTaxAsync(id, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaxById(int id, [FromBody] TaxDto? taxDto, CancellationToken cancellationToken)
        {
            await _taxService.UpdateTaxAsync(id, taxDto, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}/link")]
        public async Task<IActionResult> LinkTaxToProducts(int id, [FromBody] int[] productIdList, CancellationToken cancellationToken)
        {
            await _taxService.LinkTaxToProductsAsync(id, productIdList, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}/unlink")]
        public async Task<IActionResult> UnlinkTaxFromProducts(int id, [FromBody] int[] productIdList, CancellationToken cancellationToken)
        {
            await _taxService.UnlinkTaxFromProductsAsync(id, productIdList, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}/s_link")]
        public async Task<IActionResult> LinkTaxToServices(int id, [FromBody] int[] serviceIdList, CancellationToken cancellationToken)
        {
            await _taxService.LinkTaxToServicesAsync(id, serviceIdList, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}/s_unlink")]
        public async Task<IActionResult> UnlinkTaxFromServices(int id, [FromBody] int[] serviceIdList, CancellationToken cancellationToken)
        {
            await _taxService.UnlinkTaxFromServicesAsync(id, serviceIdList, cancellationToken);
            return Ok();
        }
    }
}