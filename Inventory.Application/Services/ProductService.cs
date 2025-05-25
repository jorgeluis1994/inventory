using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Domain.Interfaces;
using Inventory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    /// <summary>
    /// Implementación del servicio para productos.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;  // <-- Inyección de UnitOfWork

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product == null ? null : ProductDto.FromDomain(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(ProductDto.FromDomain);
        }

        public async Task AddAsync(ProductDto productDto)
        {
            var product = new Product(productDto.Name);

            // Agregar los batches al producto
            if (productDto.Batches != null)
            {
                foreach (var batchDto in productDto.Batches)
                {
                    product.AddBatch(
                        batchDto.EntryDate.ToUniversalTime(),
                        batchDto.Quantity,
                        batchDto.PriceAmount,
                        batchDto.PriceCurrency
                    );
                }
            }

            await _productRepository.AddAsync(product);

            await _unitOfWork.CommitAsync();  // Guarda cambios en la BD
        }


        public async Task UpdateAsync(ProductDto productDto)
        {
            var product = await _productRepository.GetByIdAsync(productDto.Id);
            if (product == null) throw new KeyNotFoundException("Product not found");

            if (!string.IsNullOrWhiteSpace(productDto.Name) && productDto.Name != product.Name)
            {
                product.UpdateName(productDto.Name);
            }

            await _productRepository.UpdateAsync(product);

            await _unitOfWork.CommitAsync();  // <-- Guarda cambios
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) throw new KeyNotFoundException("Product not found");

            await _productRepository.DeleteAsync(product);

            await _unitOfWork.CommitAsync();  // <-- Guarda cambios
        }

        public async Task AddBatchAsync(Guid productId, BatchDto batchDto)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null) throw new KeyNotFoundException("Product not found");

            product.AddBatch(
                batchDto.EntryDate.ToUniversalTime(),
                batchDto.Quantity,
                batchDto.PriceAmount,
                batchDto.PriceCurrency
            );

            await _productRepository.UpdateAsync(product);

            await _unitOfWork.CommitAsync();  // <-- Guarda cambios
        }
    }
}
