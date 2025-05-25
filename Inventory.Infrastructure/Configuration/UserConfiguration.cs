using Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            builder.Property<string>("_userName")
                   .HasColumnName("UserName")
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property<string>("_password")
                   .HasColumnName("Password")
                   .IsRequired();

            builder.Property<string>("_email")
                   .HasColumnName("Email")
                   .IsRequired();

            builder.Property(u => u.CreatedAt)
                   .IsRequired();

            builder.Property(u => u.UpdatedAt)
                   .IsRequired();
        }
    }
}
