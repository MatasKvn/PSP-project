using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos;
using POS_System.Business.Services;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService _productService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ApiGetAllValidProducts(CancellationToken cancellationToken)
        {
            var products = await _productService.GetAllProductsAsync(cancellationToken);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ApiGetProductById(int id, CancellationToken cancellationToken)
        {
            var products = await _productService.GetProductByIdAsync(id, cancellationToken);
            return Ok(products);
        }

        [HttpPost]
        public Task<IActionResult> ApiAddProduct(ProductDto productDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //await _productService.AddProductAsync(productDto, cancellationToken);
            //return Ok(productDto);
        }
    }
}