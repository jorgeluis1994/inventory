using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class BatchService : IBatchService
    {
        public Task<BatchDto> SaveBatch(BatchDto batch)
        {
            throw new NotImplementedException();
        }
    }
}
