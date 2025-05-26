using Inventory.Domain.Interfaces;
using Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Repositories
{
    /// <summary>
    /// Implementación del repositorio de productos con Entity Framework Core.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDbContext _context;

        public ProductRepository(InventoryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Obtiene un producto por su ID incluyendo los lotes relacionados.
        /// </summary>
        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .Include("_batches")
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Obtiene todos los productos incluyendo sus lotes.
        /// </summary>
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                        .Include("_batches")      // Incluye la propiedad privada por nombre string
                        .AsNoTracking()
                        .ToListAsync();

        }

        /// <summary>
        /// Agrega un nuevo producto (NO persiste los cambios, eso lo hará UnitOfWork).
        /// </summary>
        public async Task AddAsync(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            await _context.Products.AddAsync(product);

            // Eliminar esta línea para que UnitOfWork controle el guardado
            // await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Actualiza un producto existente (NO persiste los cambios, eso lo hará UnitOfWork).
        /// </summary>
        public async Task UpdateAsync(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            _context.Products.Update(product);

            foreach (var batch in product.Batches)
            {
                if (batch.Id == Guid.Empty)
                    _context.Entry(batch).State = EntityState.Added;
                else
                    _context.Entry(batch).State = EntityState.Modified;
            }

            // Eliminar esta línea para que UnitOfWork controle el guardado
            // await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Elimina un producto (NO persiste los cambios, eso lo hará UnitOfWork).
        /// </summary>
        public async Task DeleteAsync(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            _context.Products.Remove(product);

            // Eliminar esta línea para que UnitOfWork controle el guardado
            // await _context.SaveChangesAsync();
        }
    }
}
