using Inventory.Application.DTOs;
using Inventory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces
{
    public interface IBatchService
    {
        Task<BatchDto> SaveBatchAsync(BatchDto batchDto);
        Task<List<BatchDto>> GetAllBatchesAsync();
        Task<BatchDto?> GetBatchByIdAsync(int id);
        Task<bool> UpdateBatchAsync(BatchDto batchDto);
        Task<bool> DeleteBatchAsync(int id);
    }
}
