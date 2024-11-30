using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController(IProductService _productService) : ControllerBase
    {
        [Authorize("ItemRead")]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] bool? onlyActice, CancellationToken cancellationToken, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 0)
        {
            var products = await _productService.GetProductsAsync(pageSize, pageNumber, onlyActice, cancellationToken);
            return Ok(products);
        }

        [Authorize("ItemRead")]
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductByProductId(int productId, CancellationToken cancellationToken)
        {
            var products = await _productService.GetProductByProductIdAsync(productId, cancellationToken);
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
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProductByProductId(int productId, [FromBody] ProductRequest? productDto, CancellationToken cancellationToken)
        {
            var product = await _productService.UpdateProductByProductIdAsync(productId, productDto, cancellationToken);
            return Ok(product);
        }

        [Authorize("ItemWrite")]
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductById(int productId, CancellationToken cancellationToken)
        {
            var product = await _productService.DeleteProductByProductIdAsync(productId, cancellationToken);
            return Ok(product);
        }
    }
}