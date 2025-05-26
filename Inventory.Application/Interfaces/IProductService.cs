using Inventory.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces
{
    /// <summary>
    /// Define las operaciones relacionadas con los productos y sus lotes.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Obtiene un producto por su identificador, incluyendo sus lotes.
        /// </summary>
        /// <param name="id">Identificador del producto.</param>
        /// <returns>Datos del producto o null si no existe.</returns>
        Task<ProductDto?> GetByIdAsync(Guid id);

        /// <summary>
        /// Obtiene todos los productos con sus lotes.
        /// </summary>
        /// <returns>Lista de productos.</returns>
        Task<IEnumerable<ProductDto>> GetAllAsync();

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="productDto">Datos del producto a crear.</param>
        Task AddAsync(ProductDto productDto);

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="productDto">Datos actualizados del producto.</param>
        Task UpdateAsync(ProductDto productDto);

        /// <summary>
        /// Elimina un producto.
        /// </summary>
        /// <param name="id">Identificador del producto a eliminar.</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Agrega un lote a un producto existente.
        /// </summary>
        /// <param name="productId">Identificador del producto.</param>
        /// <param name="batchDto">Datos del lote a agregar.</param>
        Task AddBatchAsync(Guid productId, BatchDto batchDto);

        //Nueva interface para crear 
        /// <summary>
        /// Crea un nuevo producto con lotes.
        /// </summary>
        /// <param name="productCreateDto">Datos del producto a crear, incluyendo lotes.</param>
        /// Task CreateProductWithBatchesAsync(ProductCreateDto productCreateDto);
        /// summary>
        Task CreateProductWithBatchesAsync(ProductCreateDto productCreateDto);
    }
}
