using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Domain.Interfaces;
using Inventory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    
    public class BatchService : IBatchService
    {
        private readonly IBatchRepository _batchRepository;
        public BatchService(IBatchRepository batchRepository)
        {
            _batchRepository = batchRepository;
        }
        public async Task<BatchDto> SaveBatch(BatchDto batch)
        {
            // Mapeamos el DTO a entidad (modelo)
            var modelBatch = new Batch
            {
                Id = batch.Id,
                ProductId = batch.ProductId,
                BatchNumber = batch.BatchNumber,
                EntryDate = batch.EntryDate,
                Quantity = batch.Quantity
            };

            // Guardamos usando el repositorio (esperamos la tarea async)
            var savedBatch = await _batchRepository.SaveBatch(modelBatch);

            // Mapeamos de vuelta a DTO para retornar
            return new BatchDto
            {
                Id = savedBatch.Id,
                ProductId = savedBatch.ProductId,
                BatchNumber = savedBatch.BatchNumber,
                EntryDate = savedBatch.EntryDate,
                Quantity = savedBatch.Quantity
            };
        }

    }
}
