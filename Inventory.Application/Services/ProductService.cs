using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Domain.Interfaces;
using Inventory.Domain.Models;
using Microsoft.Extensions.Logging;
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

        private readonly ILogger<ProductService> _logger;
        private readonly IUnitOfWork _unitOfWork;  // <-- Inyección de UnitOfWork

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
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

            // Actualizar nombre si cambió
            if (!string.IsNullOrWhiteSpace(productDto.Name) && productDto.Name != product.Name)
            {
                product.UpdateName(productDto.Name);
            }

            // Actualizar lotes
            foreach (var batchDto in productDto.Batches)
            {
                var batch = product.Batches.FirstOrDefault(b => b.Id == batchDto.Id);

                if (batch != null)
                {
                    // Actualiza lote existente
                    batch.UpdateEntryDate(batchDto.EntryDate);
                    batch.UpdateQuantity(batchDto.Quantity);
                    batch.UpdatePrice(batchDto.PriceAmount, batchDto.PriceCurrency);
                }
                else
                {
                    // Agrega lote nuevo
                    product.AddBatch(batchDto.EntryDate, batchDto.Quantity, batchDto.PriceAmount, batchDto.PriceCurrency);
                }
            }

            await _productRepository.UpdateAsync(product);

            await _unitOfWork.CommitAsync();  // Guarda cambios
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

        public Task CreateProductWithBatchesAsync(ProductCreateDto productCreateDto)
        {
            _logger.LogInformation("Creating product with batches");
            // 2. Convertir DTO a dominio
            var product = ProductCreateDto.odomainModel(productCreateDto);
            var registeredProduct = _productRepository.AddAsync(product);
            _logger.LogInformation("Product created with ID: {ProductId}", product.Id);
            // 3. Guardar cambios en la base de datos
            _unitOfWork.CommitAsync().Wait();
            _logger.LogInformation("Changes committed to the database"); 
            

            //TODO: Validar que el producto no exista antes de agregarlo
            return Task.CompletedTask;

        }
    }
}
