using ES.BLL.Interfaces;
using ES.Common.DTOs;
using ES.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ES.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IElasticsearchProductService _productElasitcsearchService;

        public ProductController(IElasticsearchProductService elasticsearchProductService)
        {
            _productElasitcsearchService = elasticsearchProductService;
        }

        [HttpGet("GetAProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAProduct([FromQuery] string productId)
        {
            var response = await _productElasitcsearchService.GetByIdAsync(productId);

            if (response.IsValidResponse)
            {
                return Ok(response.Source);
            }
            return BadRequest();
        }

        [HttpPost("CreateProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productModel)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
            };
            var response = await _productElasitcsearchService.CreateAsync(product);

            if (response.IsValidResponse)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("GetProductByDescriptionTerm")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProductByDescriptionTerm([FromQuery] string descriptionTerm)
        {
            var response = await _productElasitcsearchService.GetProductByDescriptionTermAsync(descriptionTerm);

            if (response.IsValidResponse)
            {
                return Ok(response.Documents.FirstOrDefault());
            }
            return BadRequest();
        }

        [HttpGet("GetProductByDescriptionPhrase")]
        [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProductByDescriptionPhrase([FromQuery] string descriptionFullText)
        {
            var response = await _productElasitcsearchService.GetProductByDescriptionPhraseAsync(descriptionFullText);

            if (response.IsValidResponse)
            {
                return Ok(response.Documents.ToList());
            }
            return BadRequest();
        }

        [HttpGet("GetProductByText")]
        [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProductByText([FromQuery] string fullText, double price)
        {
            var response = await _productElasitcsearchService.GetProductByTextAsync(fullText, price);

            if (response.IsValidResponse)
            {
                return Ok(response.Documents.ToList());
            }
            return BadRequest();
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProductById([FromQuery] Guid productId, [FromBody] UpdateProductDto updateProductModel)
        {
            var product = new Product
            {
                Id = productId,
                Name = updateProductModel.Name,
                Description = updateProductModel.Description,
                Price = updateProductModel.Price,
            };
            var response = await _productElasitcsearchService.UpdateByIdAsync(productId, product);

            if (response.IsValidResponse)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteProductById([FromQuery] Guid productId)
        {
            var response = await _productElasitcsearchService.DeleteByIdAsync(productId);

            if (response.IsValidResponse)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }
    }
}
