using Inventory.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Domain.Models
{
    public class Batch
    {
        public Guid Id { get; private set; }

        private Guid _productId;
        public Guid ProductId => _productId;

        private DateTime _entryDateUtc;
        public DateTime EntryDate => _entryDateUtc;

        private int _quantity;
        public int Quantity => _quantity;

        private decimal _priceAmount;
        public decimal PriceAmount => _priceAmount;

        private string _priceCurrency;
        public string PriceCurrency => _priceCurrency;

        private Product _product;
        public Product Product => _product;

        private Batch() { }  // Constructor para EF Core

        public Batch(Product product, DateTime entryDateUtc, int quantity, decimal priceAmount, string priceCurrency)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (entryDateUtc.Kind != DateTimeKind.Utc)
                throw new ArgumentException("Entry date must be in UTC.", nameof(entryDateUtc));
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");
            if (string.IsNullOrWhiteSpace(priceCurrency))
                throw new ArgumentException("Currency is required.", nameof(priceCurrency));

            Id = Guid.NewGuid();
            _product = product;
            _productId = product.Id;
            _entryDateUtc = entryDateUtc;
            _quantity = quantity;
            _priceAmount = priceAmount;
            _priceCurrency = priceCurrency;
        }
        public void UpdateEntryDate(DateTime newDateUtc)
        {
            if (newDateUtc.Kind != DateTimeKind.Utc)
                throw new ArgumentException("Date must be UTC", nameof(newDateUtc));

            _entryDateUtc = newDateUtc;
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(newQuantity), "Quantity must be positive.");

            _quantity = newQuantity;
        }

        public void UpdatePrice(decimal newAmount, string newCurrency)
        {
            if (newAmount <= 0) throw new ArgumentOutOfRangeException(nameof(newAmount));
            if (string.IsNullOrWhiteSpace(newCurrency)) throw new ArgumentException("Currency required.");

            _priceAmount = newAmount;
            _priceCurrency = newCurrency;
        }

    }




}