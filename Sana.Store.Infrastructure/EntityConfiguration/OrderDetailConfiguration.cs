using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sana.Store.Domain;
namespace Sana.Store.Infrastructure.EntityConfiguration
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");

            builder.HasKey(e => e.Id);

            builder.Property(c => c.Quantity)
                .IsRequired(true);

            builder.Property(c => c.Price)
                .IsRequired(true)
                .HasPrecision(14, 4);

            builder
                .HasOne(u => u.Order)
                .WithMany(u => u.OrderDetails)
                .HasForeignKey(u => u.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(u => u.Product)
                .WithMany(u => u.OrderDetails)
                .HasForeignKey(u => u.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
