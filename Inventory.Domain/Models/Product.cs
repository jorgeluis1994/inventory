using System;
using System.Collections.Generic;

namespace Inventory.Domain.Models
{
    public class Product
    {
        public Guid Id { get; private set; }

        private string _name;
        public string Name => _name;

        public readonly List<Batch> _batches = new();
        public IReadOnlyCollection<Batch> Batches => _batches.AsReadOnly();

        private Product() { } // Requerido por EF Core

        public Product(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            _name = name;
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Name is required.", nameof(newName));

            _name = newName;
        }

        public void AddBatch(DateTime entryDateUtc, int quantity, decimal priceAmount, string priceCurrency)
        {
            if (entryDateUtc.Kind != DateTimeKind.Utc)
                throw new ArgumentException("Entry date must be in UTC.", nameof(entryDateUtc));
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");
            if (string.IsNullOrWhiteSpace(priceCurrency))
                throw new ArgumentException("Currency is required.", nameof(priceCurrency));

            var batch = new Batch(this, entryDateUtc, quantity, priceAmount, priceCurrency);
            _batches.Add(batch);
        }
    }
}
