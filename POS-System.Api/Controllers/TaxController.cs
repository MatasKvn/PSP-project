using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            throw new NotImplementedException();
        }
    }
}