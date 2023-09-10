using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sana.Store.Domain;
namespace Sana.Store.Infrastructure.EntityConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(e => e.Id);

            builder.Property(c => c.Number)
                .IsRequired(true);

            builder.Property(c => c.CreatedDate)
                .IsRequired(true);

            builder
                .HasOne(u => u.Customer)
                .WithMany(u => u.Orders)
                .HasForeignKey(u => u.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
