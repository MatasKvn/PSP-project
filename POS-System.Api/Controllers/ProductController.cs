using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Services;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController(IProductService _productService) : ControllerBase
    {
        [Authorize("ItemRead")]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] bool? onlyActive, CancellationToken cancellationToken, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 0)
        {
            var products = await _productService.GetProductsAsync(pageSize, pageNumber, onlyActive, cancellationToken);
            return Ok(products);
        }

        [Authorize("ItemRead")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id, CancellationToken cancellationToken)
        {
            var products = await _productService.GetProductByIdAsync(id, cancellationToken);
            return Ok(products);
        }

        [Authorize("ItemRead")]
        [HttpGet("{productId}/versions/")]
        public async Task<IActionResult> GetProductVersionsByProductId(int productId, CancellationToken cancellationToken)
        {
            var products = await _productService.GetProductVersionsByProductIdAsync(productId, cancellationToken);
            return Ok(products);
        }

        [Authorize("ItemWrite")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest? productDto, CancellationToken cancellationToken)
        {
            var product = await _productService.CreateProductAsync(productDto, cancellationToken);
            return Ok(product);
        }

        [Authorize("ItemWrite")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductByProductId(int id, [FromBody] ProductRequest? productDto, CancellationToken cancellationToken)
        {
            var product = await _productService.UpdateProductByIdAsync(id, productDto, cancellationToken);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize("ItemWrite")]
        public async Task<IActionResult> DeleteProductById(int id, CancellationToken cancellationToken)
        {
            var product = await _productService.DeleteProductByIdAsync(id, cancellationToken);
            return Ok(product);
        }

        //Leave timeStamp null if you want to get only the active items
        [HttpGet("tax/{id}")]
        [Authorize(Policy = "ItemRead")]
        public async Task<IActionResult> GetProductsLinkedToTaxId(int id, [FromQuery] DateTime? timeStamp, CancellationToken cancellationToken)
        {
            var products = await _productService.GetProductsLinkedToTaxId(id, timeStamp, cancellationToken);
            return Ok(products);
        }

        //Leave timeStamp null if you want to get only the active items
        [HttpGet("item-discount/{id}")]
        [Authorize(Policy = "ItemRead")]
        public async Task<IActionResult> GetProductsLinkedToItemDiscountId(int id, [FromQuery] DateTime? timeStamp, CancellationToken cancellationToken)
        {
            var products = await _productService.GetProductsLinkedToItemDiscountId(id, timeStamp, cancellationToken);
            return Ok(products);
        }
    }
}