using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sana.Store.Domain;
namespace Sana.Store.Infrastructure.EntityConfiguration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(e => e.Id);

            builder.Property(c => c.DocumentNumber)
                .IsRequired(true)
                .HasMaxLength(20);

            builder.Property(c => c.Name)
                .IsRequired(true)
                .HasMaxLength(150);

            builder.Property(c => c.Address)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.Property(c => c.Phone)
                .IsRequired(false)
                .HasMaxLength(20);
        }
    }
}
