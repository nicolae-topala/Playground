using Microsoft.AspNetCore.Mvc;
using WebApp.BLL.Interfaces;
using WebApp.Common.DTOs.Customer;
using WebApp.Common.DTOs.Product;

namespace WebApp.API.Controllers
{
    [ApiController]
    [Route("api/Product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("all-products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetAllCustomersAsync();
            return Ok(result);
        }

        [HttpPost("product")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateProductDto productDto)
        {
            var result = await _productService.AddProductAsync(productDto);
            return Ok(result);
        }
    }
}
