using Inventory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> SaveProduct(Product product);

        Task<List<Product>> GetProducts();
        Task<bool> DeleteProduct(int id);
    }
}
