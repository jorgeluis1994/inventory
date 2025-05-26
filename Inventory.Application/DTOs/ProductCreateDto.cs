using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Inventory.Application.DTOs
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public List<BatchCreateDto> Batches { get; set; } = new List<BatchCreateDto>();

        public static Product odomainModel(ProductCreateDto dto)
        {
            Product product = new Product(dto.Name);

            foreach (var batch in dto.Batches)
            {
                product.AddBatch(
                    batch.EntryDate.ToUniversalTime(),
                    batch.Quantity,
                    batch.PriceAmount,
                    batch.PriceCurrency);
            }

            return product;
            
            
        }

       
    }

   public class BatchCreateDto
    {
        public DateTime EntryDate { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAmount { get; set; }
        public string PriceCurrency { get; set; } = null!;
    }
}