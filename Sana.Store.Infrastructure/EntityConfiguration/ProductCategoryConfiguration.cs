using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sana.Store.Domain;
namespace Sana.Store.Infrastructure.EntityConfiguration
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");

            builder.HasKey(e => new { e.ProductId, e.CategoryId });

            builder
                .HasOne(u => u.Product)
                .WithMany(u => u.ProductCategories)
                .HasForeignKey(u => u.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(u => u.Category)
                .WithMany(u => u.ProductCategories)
                .HasForeignKey(u => u.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
