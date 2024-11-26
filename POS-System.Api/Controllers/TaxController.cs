using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Tax;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [ApiController]
    [Route("api/tax")]
    public class TaxController(ITaxService _taxService, IProductOnTaxService _productOnTaxService, IServiceOnTaxService _serviceOnTaxService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTaxes (CancellationToken cancellationToken)
        {
            var taxes = await _taxService.GetAllTaxesAsync(cancellationToken);
            return Ok(taxes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTax([FromBody] TaxRequestDto taxDto, CancellationToken cancellationToken)
        {
            var createdTax = await _taxService.CreateTaxAsync(taxDto, cancellationToken);
            return Ok(createdTax);
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
        public async Task<IActionResult> UpdateTaxById(int id, [FromBody] TaxRequestDto taxDto, CancellationToken cancellationToken)
        {
            var updatedTax = await _taxService.UpdateTaxAsync(id, taxDto, cancellationToken);
            return Ok(updatedTax);
        }

        [HttpPut("{id}/p_link")]
        public async Task<IActionResult> LinkTaxToProducts(int id, [FromBody] int[] productIdList, CancellationToken cancellationToken)
        {
            await _productOnTaxService.LinkTaxToProductsAsync(id, productIdList, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}/p_unlink")]
        public async Task<IActionResult> UnlinkTaxFromProducts(int id, [FromBody] int[] productIdList, CancellationToken cancellationToken)
        {
            await _productOnTaxService.UnlinkTaxFromProductsAsync(id, productIdList, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}/s_link")]
        public async Task<IActionResult> LinkTaxToServices(int id, [FromBody] int[] serviceIdList, CancellationToken cancellationToken)
        {
            await _serviceOnTaxService.LinkTaxToServicesAsync(id, serviceIdList, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}/s_unlink")]
        public async Task<IActionResult> UnlinkTaxFromServices(int id, [FromBody] int[] serviceIdList, CancellationToken cancellationToken)
        {
            await _serviceOnTaxService.UnlinkTaxFromServicesAsync(id, serviceIdList, cancellationToken);
            return Ok();
        }
    }
}