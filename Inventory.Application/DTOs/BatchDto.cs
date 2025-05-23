using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.DTOs
{
    public class BatchDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? BatchNumber { get; set; }
        public DateTime EntryDate { get; set; }
        public int Quantity { get; set; }
    }
}
