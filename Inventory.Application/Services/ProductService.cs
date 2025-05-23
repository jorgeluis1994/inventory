using Inventory.API.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Domain.Interfaces;
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

        public Task<ProductDto> SaveProduct(ProductDto product)
        {
            throw new NotImplementedException();
        }
    }
}
