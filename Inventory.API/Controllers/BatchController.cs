using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BatchesController : ControllerBase
    {
        private readonly IBatchService _batchService;

        public BatchesController(IBatchService batchService)
        {
            _batchService = batchService;
        }

        // GET: api/batches
        [HttpGet]
        public async Task<ActionResult<List<BatchDto>>> GetAll()
        {
            var batches = await _batchService.GetAllBatchesAsync();
            return Ok(batches);
        }

        // GET: api/batches/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BatchDto>> GetById(int id)
        {
            var batch = await _batchService.GetBatchByIdAsync(id);
            if (batch == null)
                return NotFound();

            return Ok(batch);
        }

        // POST: api/batches
        [HttpPost]
        public async Task<ActionResult<BatchDto>> Create([FromBody] BatchDto batchDto)
        {
            if (batchDto == null)
                return BadRequest("Batch data is null.");

            var createdBatch = await _batchService.SaveBatchAsync(batchDto);
            return CreatedAtAction(nameof(GetById), new { id = createdBatch.Id }, createdBatch);
        }

        // PUT: api/batches/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] BatchDto batchDto)
        {
            if (batchDto == null || batchDto.Id != id)
                return BadRequest("Batch data is invalid.");

            var existingBatch = await _batchService.GetBatchByIdAsync(id);
            if (existingBatch == null)
                return NotFound();

            var updated = await _batchService.UpdateBatchAsync(batchDto);
            if (!updated)
                return StatusCode(500, "Error updating batch.");

            return NoContent();
        }

        // DELETE: api/batches/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingBatch = await _batchService.GetBatchByIdAsync(id);
            if (existingBatch == null)
                return NotFound();

            var deleted = await _batchService.DeleteBatchAsync(id);
            if (!deleted)
                return StatusCode(500, "Error deleting batch.");

            return NoContent();
        }
    }
}
