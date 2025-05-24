using Inventory.API.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Domain.Interfaces;
using Inventory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<bool> DeleteProduct(int id)
        {
            return _productRepository.DeleteProduct(id);
        }

        public async Task<List<ProductDto>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            }).ToList();
        }

        public async Task<ProductDto> SaveProduct(ProductDto product)
        {
            
            var  productModel = new Product
            {
                Name = product.Name,
                Description = product.Description
            };

            var savedProduct = await _productRepository.SaveProduct(productModel);
            return new ProductDto
            {
                Id = savedProduct.Id,
                Name = savedProduct.Name,
                Description = savedProduct.Description
            };




        }
    }
}
