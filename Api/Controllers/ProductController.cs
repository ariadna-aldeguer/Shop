using Api.Models;
using Api.Models.Commands.Product;
using Api.Models.Dtos;
using Api.Models.Queries;
using Api.Services;
using Api.Services.Interficies;
using Data.Database.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProductDto>>> GetProducts([FromQuery] ProductQuery productQuery)
        {
            return Ok(await _productService.GetFilteredProductsAsync(productQuery));
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] ProductCommand productInsert)
        {
            var finalProduct = await _productService.AddOrUpdateAsync(productInsert);
            return Ok(finalProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductCommand productUpdate)
        {
            var finalProduct = await _productService.AddOrUpdateAsync(productUpdate, id);
            return Ok(finalProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var deletedProduct = await _productService.DeleteAsync(id);
            return Ok(deletedProduct);
        }

    }
}
