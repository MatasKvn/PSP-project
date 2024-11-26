using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.ProductModificationDtos;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [Route("api/product-modification")]
    [ApiController]
    public class ProductModificationController(IProductModificationService _productModificationService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProductModifications(CancellationToken cancellationToken)
        {
            var productModifications = await _productModificationService.GetAllProductModificationsAsync(cancellationToken);
            return Ok(productModifications);
        }

        [HttpGet("{productModificationId}")]
        public async Task<IActionResult> GetProductModificationByProductModificationId(int productModificationId, CancellationToken cancellationToken)
        {
            var productModifications = await _productModificationService.GetProductModificationByProductModificationIdAsync(productModificationId, cancellationToken);
            return Ok(productModifications);
        }

        [HttpGet("{productModificationtId}/versions/")]
        public async Task<IActionResult> GetProductModificationVersionsByProductModificationId(int productModificationtId, CancellationToken cancellationToken)
        {
            var productModifications = await _productModificationService.GetProductModificationVersionsByProductModificationIdAsync(productModificationtId, cancellationToken);
            return Ok(productModifications);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductModification([FromBody] CreateProductModificationDto? productModificationDto, CancellationToken cancellationToken)
        {
            var productModification = await _productModificationService.CreateProductModificationAsync(productModificationDto, cancellationToken);
            return Ok(productModification);
        }

        [HttpPut("{productModificationId}")]
        public async Task<IActionResult> UpdateProductModification(int productModificationId, [FromBody] CreateProductModificationDto? productModificationDto, CancellationToken cancellationToken)
        {
            var productModification = await _productModificationService.UpdateProductModificationAsync(productModificationId, productModificationDto, cancellationToken);
            return Ok(productModification);
        }

        [HttpDelete("{productModificationId}")]
        public async Task<IActionResult> DeleteProductModification(int productModificationId, CancellationToken cancellationToken)
        {
            var productModification = await _productModificationService.DeleteProductModificationAsync(productModificationId, cancellationToken);
            return Ok(productModification);
        }
    }
}
