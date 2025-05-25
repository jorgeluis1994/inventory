using Inventory.Domain.Models;
using System;

namespace Inventory.Application.DTOs
{
    public class BatchDto
    {
        public Guid Id { get; set; }
        public DateTime EntryDate { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAmount { get; set; }
        public string PriceCurrency { get; set; }

        /// <summary>
        /// Convierte una entidad Batch a BatchDto.
        /// </summary>
        public static BatchDto FromDomain(Batch batch)
        {
            return new BatchDto
            {
                Id = batch.Id,
                EntryDate = batch.EntryDate,
                Quantity = batch.Quantity,
                PriceAmount = batch.PriceAmount,
                PriceCurrency = batch.PriceCurrency
            };
        }

        /// <summary>
        /// Convierte un BatchDto a entidad Batch, requiere la instancia Product.
        /// </summary>
        public Batch ToDomain(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            // Nota: El constructor Batch es internal y requiere Product, EntryDate, Quantity, PriceAmount, PriceCurrency
            return new Batch(
                product,
                EntryDate,
                Quantity,
                PriceAmount,
                PriceCurrency
            );
        }
    }
}
