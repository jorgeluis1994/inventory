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

        private Batch MapToEntity(BatchDto dto)
        {
            return new Batch
            {
                Id = dto.Id,
                ProductId = dto.ProductId,
                BatchNumber = dto.BatchNumber,
                EntryDate = dto.EntryDate,
                Quantity = dto.Quantity
            };
        }

        private BatchDto MapToDto(Batch entity)
        {
            return new BatchDto
            {
                Id = entity.Id,
                ProductId = entity.ProductId,
                BatchNumber = entity.BatchNumber,
                EntryDate = entity.EntryDate,
                Quantity = entity.Quantity
            };
        }

        public async Task<BatchDto> SaveBatchAsync(BatchDto batchDto)
        {
            var batch = MapToEntity(batchDto);
            var savedBatch = await _batchRepository.SaveBatch(batch);
            return MapToDto(savedBatch);
        }

        public async Task<List<BatchDto>> GetAllBatchesAsync()
        {
            var batches = await _batchRepository.GetAllAsync();
            return batches.Select(MapToDto).ToList();
        }

        public async Task<BatchDto?> GetBatchByIdAsync(int id)
        {
            var batch = await _batchRepository.GetByIdAsync(id);
            if (batch == null) return null;
            return MapToDto(batch);
        }

        public async Task<bool> UpdateBatchAsync(BatchDto batchDto)
        {
            var batch = MapToEntity(batchDto);
            return await _batchRepository.UpdateAsync(batch);
        }

        public async Task<bool> DeleteBatchAsync(int id)
        {
            return await _batchRepository.DeleteAsync(id);
        }
    }

}
