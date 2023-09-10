﻿using Microsoft.EntityFrameworkCore;
using Sana.Store.Domain;

namespace Sana.Store.Infrastructure
{
    public class ServiceSeeding
    {
        public async Task SeedAsync(ServiceDbContext context)
        {
            await context.Database.MigrateAsync();

            if (!await context.Categories.AnyAsync())
            {
                var categories = new List<Category>()
                {
                    new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = "Home"
                    },
                    new Category
                    {
                        Id= Guid.NewGuid(),
                        Name = "Cars"
                    }
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            if (!await context.Customers.AnyAsync())
            {
                await context.Customers.AddAsync(new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Generic",
                    DocumentNumber = "9999999999"
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
