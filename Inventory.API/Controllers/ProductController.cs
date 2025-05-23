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

    }
}
