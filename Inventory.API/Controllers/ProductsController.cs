using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        // GET api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {

            var authHeader = Request.Headers["Authorization"].ToString();
            _logger.LogInformation("Authorization header: {AuthHeader}", authHeader);
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        // GET api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST api/products
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductDto productDto)
        {
            if (productDto == null)
                return BadRequest();

            await _productService.AddAsync(productDto);

            // Aquí podrías retornar el recurso creado, con su URI
            return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);
        }

        // PUT api/products/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] ProductDto productDto)
        {
            if (productDto == null || productDto.Id != id)
                return BadRequest();

            try
            {
                await _productService.UpdateAsync(productDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/products/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _productService.DeleteAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST api/products/{id}/batches
        [HttpPost("{id}/batches")]
        public async Task<ActionResult> AddBatch(Guid id, [FromBody] BatchDto batchDto)
        {
            if (batchDto == null)
                return BadRequest();

            try
            {
                await _productService.AddBatchAsync(id, batchDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("addProduct")]
        public async Task<ActionResult> addProduct([FromBody] ProductCreateDto productDto)
        {
            if (productDto == null)
                return BadRequest();

            await _productService.CreateProductWithBatchesAsync(productDto);

            // Aquí podrías retornar el recurso creado, con su URI
            return CreatedAtAction(nameof(GetById), new { id = productDto.Name }, productDto);
        }


    }
}
