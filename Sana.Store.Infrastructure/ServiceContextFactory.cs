using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Sana.Store.Infrastructure
{
    public class ServiceContextFactory : IDesignTimeDbContextFactory<ServiceDbContext>
    {
        public ServiceDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ServiceDbContext>();

            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=SanaStore;User Id=sa;password=Test123!;TrustServerCertificate=true");

            return new ServiceDbContext(optionsBuilder.Options);
        }
    }
}
