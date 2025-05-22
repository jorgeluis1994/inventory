using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Domain.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int BatchId { get; private set; }
        public Batch Batch { get; private set; } = null!;

        public string TransactionType { get; private set; } = null!; // "IN", "OUT", "ADJUSTMENT"
        public int Quantity { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public string? Notes { get; private set; }

    }
}