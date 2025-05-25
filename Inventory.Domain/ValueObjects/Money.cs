using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.ValueObjects
{
    
    public class Money
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; } = "USD"; // o configurable

        private Money() { }

        public Money(decimal amount, string currency = "USD")
        {
            if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");
            Amount = amount;
            Currency = currency;
        }

        // Value object equality
        public override bool Equals(object? obj) =>
            obj is Money money && Amount == money.Amount && Currency == money.Currency;

        public override int GetHashCode() => HashCode.Combine(Amount, Currency);
    }

}
