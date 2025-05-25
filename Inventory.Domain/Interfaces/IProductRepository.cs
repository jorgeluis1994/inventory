using Inventory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Interfaces
{
    /// <summary>
    /// Repositorio para gestionar las operaciones CRUD sobre productos.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Obtiene un producto por su identificador, incluyendo sus lotes.
        /// </summary>
        /// <param name="id">Identificador único del producto.</param>
        /// <returns>Producto si existe, o null si no se encuentra.</returns>
        Task<Product?> GetByIdAsync(Guid id);

        /// <summary>
        /// Obtiene todos los productos disponibles.
        /// </summary>
        /// <returns>Lista de productos.</returns>
        Task<IEnumerable<Product>> GetAllAsync();

        /// <summary>
        /// Agrega un nuevo producto.
        /// </summary>
        /// <param name="product">Producto a agregar.</param>
        Task AddAsync(Product product);

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="product">Producto con los cambios a actualizar.</param>
        Task UpdateAsync(Product product);

        /// <summary>
        /// Elimina un producto.
        /// </summary>
        /// <param name="product">Producto a eliminar.</param>
        Task DeleteAsync(Product product);
    }
}
