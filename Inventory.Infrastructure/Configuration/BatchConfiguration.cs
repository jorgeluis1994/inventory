using Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Inventory.Infrastructure.Configurations
{
    public class BatchConfiguration : IEntityTypeConfiguration<Batch>
    {
        public void Configure(EntityTypeBuilder<Batch> builder)
        {
            builder.ToTable("Batches");
            builder.HasKey(b => b.Id);

            builder.Property<Guid>("_productId")
                   .HasColumnName("ProductId")
                   .IsRequired();

            builder.Property<DateTime>("_entryDateUtc")
                   .HasColumnName("EntryDate")
                   .IsRequired();

            builder.Property<int>("_quantity")
                   .HasColumnName("Quantity")
                   .IsRequired();

            builder.Property<decimal>("_priceAmount")
                   .HasColumnName("PriceAmount")
                   .IsRequired();

            builder.Property<string>("_priceCurrency")
                   .HasColumnName("PriceCurrency")
                   .HasMaxLength(3)
                   .IsRequired();

            // Relación con Product usando campo privado "_product"
            builder.HasOne(typeof(Product), "_product")
                   .WithMany("_batches")
                   .HasForeignKey("_productId")
                   .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
