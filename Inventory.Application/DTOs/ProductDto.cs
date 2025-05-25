using Inventory.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.Application.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<BatchDto> Batches { get; set; } = new();

        /// <summary>
        /// Convierte una entidad Product a ProductDto.
        /// </summary>
        public static ProductDto FromDomain(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Batches = product.Batches.Select(BatchDto.FromDomain).ToList()
            };
        }

        /// <summary>
        /// Convierte un ProductDto a entidad Product.
        /// </summary>
        public Product ToDomain()
        {
            var product = new Product(Name);

            // Si necesitas asignar Id explícitamente y tienes método, hazlo aquí

            foreach (var batchDto in Batches)
            {
                // Usamos AddBatch que acepta los campos separados
                product.AddBatch(
                    batchDto.EntryDate,
                    batchDto.Quantity,
                    batchDto.PriceAmount,
                    batchDto.PriceCurrency
                );
            }

            return product;
        }
    }
}
