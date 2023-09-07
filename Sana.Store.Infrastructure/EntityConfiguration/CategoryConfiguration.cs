using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sana.Store.Domain;

namespace Sana.Store.Infrastructure.EntityConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(e => e.Id);

            builder.Property(c => c.Name)
                .IsRequired(true)
                .HasMaxLength(150);
        }
    }
}
