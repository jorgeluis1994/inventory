using Inventory.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inventory.Application.DTOs;
using Inventory.API.DTOs;
using Inventory.Application.Interfaces;

namespace Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProducts();
            return Ok(products);
        }


        [HttpPost]
        public async Task<IActionResult> SaveProduct([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Product data is null");
            }
            else
            {
                return Ok(await _productService.SaveProduct(productDto));
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);

            if (!result)
                return NotFound(); 

            return NoContent(); 
        }


    }
}
