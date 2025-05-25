using Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);

            builder.Property<string>("_name")
                   .HasColumnName("Name")
                   .IsRequired()
                   .HasMaxLength(100);

            // Ignoramos la propiedad pública para evitar conflictos
            builder.Ignore(p => p.Batches);

            // Mapeamos la colección usando el campo privado
            builder.HasMany(typeof(Batch), "_batches")
                   .WithOne("_product")
                   .HasForeignKey("_productId")
                   .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
