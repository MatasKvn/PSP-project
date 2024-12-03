using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [ApiController]
    [Route("api/tax")]
    public class TaxController(ITaxService _taxService) : ControllerBase
    {
        [HttpGet]
        [Authorize("TaxRead")]
        public async Task<IActionResult> GetAllTaxes (CancellationToken cancellationToken, [FromQuery] int pageNum = 0, [FromQuery] int pageSize = 10)
        {
            var taxes = await _taxService.GetAllTaxesAsync(pageSize, pageNum, cancellationToken);
            return Ok(taxes);
        }

        [HttpPost]
        [Authorize("TaxWrite")]
        public async Task<IActionResult> CreateTax([FromBody] TaxRequest taxDto, CancellationToken cancellationToken)
        {
            var createdTax = await _taxService.CreateTaxAsync(taxDto, cancellationToken);
            return Ok(createdTax);
        }

        [HttpGet("{id}")]
        [Authorize("TaxRead")]
        public async Task<IActionResult> GetTaxById(int id, CancellationToken cancellationToken)
        {
            var tax = await _taxService.GetTaxByIdAsync(id, cancellationToken);
            return Ok(tax);
        }

        [HttpDelete("{id}")]
        [Authorize("TaxWrite")]
        public async Task<IActionResult> DeleteTaxById(int id, CancellationToken cancellationToken)
        {
            await _taxService.DeleteTaxAsync(id, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize("TaxWrite")]
        public async Task<IActionResult> UpdateTaxById(int id, [FromBody] TaxRequest taxDto, CancellationToken cancellationToken)
        {
            var updatedTax = await _taxService.UpdateTaxAsync(id, taxDto, cancellationToken);
            return Ok(updatedTax);
        }

        [HttpPut("{id}/link")]
        [Authorize("TaxWrite")]
        public async Task<IActionResult> LinkTaxToItems(int id, [FromQuery] bool itemsAreProducts, [FromBody] int[] itemIdList, CancellationToken cancellationToken)
        {
            await _taxService.LinkTaxToItemsAsync(id, itemsAreProducts, itemIdList, cancellationToken);
                
            return Ok();
        }

        [HttpPut("{id}/unlink")]
        [Authorize("TaxWrite")]
        public async Task<IActionResult> UnlinkTaxFromProducts(int id, [FromQuery] bool itemsAreProducts, [FromBody] int[] itemIdList, CancellationToken cancellationToken)
        {
            await _taxService.UnlinkTaxFromItemsAsync(id, itemsAreProducts, itemIdList, cancellationToken);
            
            return Ok();
        }

        //Leave timeStamp null if you want to get only the active items
        [HttpGet("product/{id}")]
        [Authorize("TaxRead")]
        public async Task<IActionResult> GetTaxesLinkedToItemId(int id, [FromQuery] bool isProduct, [FromQuery] DateTime? timeStamp, CancellationToken cancellationToken)
        {
            var taxes = await _taxService.GetTaxesLinkedToItemId(id, isProduct, timeStamp, cancellationToken); 
            return Ok(taxes);
        }
    }
}