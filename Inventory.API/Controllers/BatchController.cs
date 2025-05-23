using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {

        private readonly IBatchService _batchService;
        public BatchController(IBatchService batchService)
        {
            _batchService = batchService;
        }
        
        /// <summary>

        [HttpPost]
        public async Task<IActionResult> SaveBatch([FromBody] BatchDto batchDto)
        {
            if(batchDto == null)
            {
                return BadRequest("Batch data is null");

            }
            else
            {
                return Ok(await _batchService.SaveBatch(batchDto));
            }
      
        }
    }
}
