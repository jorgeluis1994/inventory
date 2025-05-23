using Inventory.Domain.Interfaces;
using Inventory.Domain.Models;
using Inventory.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Repositories
{
    public class BatchRepository : IBatchRepository
    {
        private readonly InventoryDbContext _context;
        public BatchRepository(InventoryDbContext context)
        {
            _context = context;
        }
        public async Task<Batch> SaveBatch(Batch batch)
        {
            await _context.Batches.AddAsync(batch);
            await _context.SaveChangesAsync();
            return batch;
        }
    }
}
