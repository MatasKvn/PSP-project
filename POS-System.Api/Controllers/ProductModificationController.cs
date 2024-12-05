using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [Route("api/product-modification")]
    [ApiController]
    public class ProductModificationController(IProductModificationService _productModificationService) : ControllerBase
    {
        [Authorize("ItemRead")]
        [HttpGet]
        public async Task<IActionResult> GetAllProductModifications([FromQuery] bool? onlyActive, CancellationToken cancellationToken, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 0)
        {
            var productModifications = await _productModificationService.GetProductModificationsAsync(pageSize, pageNumber, onlyActive, cancellationToken);
            return Ok(productModifications);
        }

        [Authorize("ItemRead")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductModificationById(int id, CancellationToken cancellationToken)
        {
            var productModifications = await _productModificationService.GetProductModificationByIdAsync(id, cancellationToken);
            return Ok(productModifications);
        }

        [Authorize("ItemRead")]
        [HttpGet("{productModificationtId}/versions/")]
        public async Task<IActionResult> GetProductModificationVersionsByProductModificationId(int productModificationtId, CancellationToken cancellationToken)
        {
            var productModifications = await _productModificationService.GetProductModificationVersionsByProductModificationIdAsync(productModificationtId, cancellationToken);
            return Ok(productModifications);
        }

        [Authorize("ItemWrite")]
        [HttpPost]
        public async Task<IActionResult> CreateProductModification([FromBody] ProductModificationRequest? productModificationDto, CancellationToken cancellationToken)
        {
            var productModification = await _productModificationService.CreateProductModificationAsync(productModificationDto, cancellationToken);
            return Ok(productModification);
        }

        [Authorize("ItemWrite")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductModification(int id, [FromBody] ProductModificationRequest? productModificationDto, CancellationToken cancellationToken)
        {
            var productModification = await _productModificationService.UpdateProductModificationByIdAsync(id, productModificationDto, cancellationToken);
            return Ok(productModification);
        }

        [Authorize("ItemWrite")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductModification(int id, CancellationToken cancellationToken)
        {
            var productModification = await _productModificationService.DeleteProductModificationByIdAsync(id, cancellationToken);
            return Ok(productModification);
        }
    }
}
