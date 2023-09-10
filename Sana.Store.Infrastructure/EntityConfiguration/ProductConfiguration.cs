using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sana.Store.Domain;

namespace Sana.Store.Infrastructure.EntityConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(e => e.Id);

            builder.Property(c => c.Title)
                .IsRequired(true)
                .HasMaxLength(150);

            builder.Property(c => c.Code)
                .IsRequired(true)
                .HasMaxLength(20);

            builder.Property(c => c.Description)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.Property(c => c.Price)
                .IsRequired(true)
                .HasPrecision(14, 4);

            builder.Property(c => c.AvailableStock)
                .IsRequired(true)
                .HasDefaultValue(0);
        }
    }
}
