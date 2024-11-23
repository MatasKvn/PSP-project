using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.ProductDtos;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController(IProductService _productService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
        {
            var products = await _productService.GetAllProductsAsync(cancellationToken);
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductByProductId(int productId, CancellationToken cancellationToken)
        {
            var products = await _productService.GetProductByProductIdAsync(productId, cancellationToken);
            return Ok(products);
        }

        [HttpGet("{productId}/versions/")]
        public async Task<IActionResult> GetProductVersionsByProductId(int productId, CancellationToken cancellationToken)
        {
            var products = await _productService.GetProductVersionsByProductIdAsync(productId, cancellationToken);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto? productDto, CancellationToken cancellationToken)
        {
            var product = await _productService.CreateProductAsync(productDto, cancellationToken);
            return Ok(product);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProductByProductId(int productId, [FromBody] CreateProductDto? productDto, CancellationToken cancellationToken)
        {
            var product = await _productService.UpdateProductByProductIdAsync(productId, productDto, cancellationToken);
            return Ok(product);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductById(int productId, CancellationToken cancellationToken)
        {
            await _productService.DeleteProductByProductIdAsync(productId, cancellationToken);
            return Ok();
        }
    }
}