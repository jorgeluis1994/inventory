using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Domain.Models
{
    public class Batch
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public string? BatchNumber { get; set; }
        public DateTime EntryDate { get; set; }
        public int Quantity { get; set; }
    }
}