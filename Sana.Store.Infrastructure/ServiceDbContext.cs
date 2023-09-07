using Microsoft.EntityFrameworkCore;
using Sana.Store.Domain;

namespace Sana.Store.Infrastructure
{
    public class ServiceDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public ServiceDbContext(DbContextOptions<ServiceDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
