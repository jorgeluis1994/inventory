using Inventory.Domain.Interfaces;
using Inventory.Domain.Models;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Batch>> GetAllAsync()
        {
            return await _context.Batches.Include(b => b.Product).ToListAsync();
        }

        public async Task<Batch?> GetByIdAsync(int id)
        {
            return await _context.Batches.Include(b => b.Product).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> UpdateAsync(Batch batch)
        {
            var existingBatch = await _context.Batches.FindAsync(batch.Id);
            if (existingBatch == null) return false;

            existingBatch.ProductId = batch.ProductId;
            existingBatch.BatchNumber = batch.BatchNumber;
            existingBatch.EntryDate = batch.EntryDate;
            existingBatch.Quantity = batch.Quantity;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var batch = await _context.Batches.FindAsync(id);
            if (batch == null) return false;

            _context.Batches.Remove(batch);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
