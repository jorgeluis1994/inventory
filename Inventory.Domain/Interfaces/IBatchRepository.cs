using Inventory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Interfaces
{
    public interface IBatchRepository
    {
        Task<Batch> SaveBatch(Batch batch);
        Task<List<Batch>> GetAllAsync();
        Task<Batch?> GetByIdAsync(int id); 
        Task<bool> UpdateAsync(Batch batch);
        Task<bool> DeleteAsync(int id);
    }
}
