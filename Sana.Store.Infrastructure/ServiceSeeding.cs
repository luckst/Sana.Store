using Microsoft.EntityFrameworkCore;
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
                        Id = Guid.Parse("AF5251CB-13BA-4D9A-96DF-3A3E32D57DDB"),
                        Name = "Home"
                    },
                    new Category
                    {
                        Id= Guid.Parse("C1C2EF6C-110B-4050-9387-8F4060FBD4AA"),
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

            if (!await context.Products.AnyAsync())
            {
                var products = new List<Product>()
                {
                    new Product
                    {
                        Id = Guid.NewGuid() ,
                        AvailableStock = 10 ,
                        Code="ABC1234",
                        Description="Total cleaner",
                        Price=400.5m,
                        Title="Total cleaner",
                        ProductCategories = new List<ProductCategory>()
                        {
                            new ProductCategory
                            {
                                CategoryId =Guid.Parse("AF5251CB-13BA-4D9A-96DF-3A3E32D57DDB")
                            },
                            new ProductCategory
                            {
                                CategoryId =Guid.Parse("C1C2EF6C-110B-4050-9387-8F4060FBD4AA")
                            }
                        }
                    },
                    new Product
                    {
                        Id = Guid.NewGuid() ,
                        AvailableStock = 100,
                        Code="ABC1235",
                        Description="The best shampoo for cars",
                        Price=50,
                        Title="Star Shampoo",
                        ProductCategories = new List<ProductCategory>()
                        {
                            new ProductCategory
                            {
                                CategoryId =Guid.Parse("C1C2EF6C-110B-4050-9387-8F4060FBD4AA")
                            }
                        }
                    },
                    new Product
                    {
                        Id = Guid.NewGuid() ,
                        AvailableStock = 5000,
                        Code="ABC1236",
                        Description="Ceramic Pan",
                        Price=100.25m,
                        Title="Ceramic Pan",
                        ProductCategories = new List<ProductCategory>()
                        {
                            new ProductCategory
                            {
                                CategoryId =Guid.Parse("AF5251CB-13BA-4D9A-96DF-3A3E32D57DDB")
                            }
                        }
                    },
                    new Product
                    {
                        Id = Guid.NewGuid() ,
                        AvailableStock = 5000,
                        Code="ABC1237",
                        Description="Ceramic Pan aaaaaaaaaaaaaaaa",
                        Price=100.25m,
                        Title="Ceramic Pan A",
                        ProductCategories = new List<ProductCategory>()
                        {
                            new ProductCategory
                            {
                                CategoryId =Guid.Parse("AF5251CB-13BA-4D9A-96DF-3A3E32D57DDB")
                            }
                        }
                    },
                    new Product
                    {
                        Id = Guid.NewGuid() ,
                        AvailableStock = 5000,
                        Code="ABC1238",
                        Description="Ceramic Pan bbbbbbbbbbbbb",
                        Price=100.25m,
                        Title="Ceramic Pan B",
                        ProductCategories = new List<ProductCategory>()
                        {
                            new ProductCategory
                            {
                                CategoryId =Guid.Parse("AF5251CB-13BA-4D9A-96DF-3A3E32D57DDB")
                            }
                        }
                    },
                    new Product
                    {
                        Id = Guid.NewGuid() ,
                        AvailableStock = 100,
                        Code="ABC1241",
                        Description="The best shampoo for cars aaaaaaaaaaaaa",
                        Price=50,
                        Title="Star Shampoo A",
                        ProductCategories = new List<ProductCategory>()
                        {
                            new ProductCategory
                            {
                                CategoryId =Guid.Parse("C1C2EF6C-110B-4050-9387-8F4060FBD4AA")
                            }
                        }
                    },
                    new Product
                    {
                        Id = Guid.NewGuid() ,
                        AvailableStock = 100,
                        Code="ABC1242",
                        Description="The best shampoo for cars bbbbbbbbbbbbb",
                        Price=50,
                        Title="Star Shampoo B",
                        ProductCategories = new List<ProductCategory>()
                        {
                            new ProductCategory
                            {
                                CategoryId =Guid.Parse("C1C2EF6C-110B-4050-9387-8F4060FBD4AA")
                            }
                        }
                    },
                    new Product
                    {
                        Id = Guid.NewGuid() ,
                        AvailableStock = 100,
                        Code="ABC1243",
                        Description="The best shampoo for cars cccccccccc",
                        Price=50,
                        Title="Star Shampoo C",
                        ProductCategories = new List<ProductCategory>()
                        {
                            new ProductCategory
                            {
                                CategoryId =Guid.Parse("C1C2EF6C-110B-4050-9387-8F4060FBD4AA")
                            }
                        }
                    },
                    new Product
                    {
                        Id = Guid.NewGuid() ,
                        AvailableStock = 10 ,
                        Code="ABC1251",
                        Description="Total cleaner aaaaaaaaaa",
                        Price=400.5m,
                        Title="Total cleaner A",
                        ProductCategories = new List<ProductCategory>()
                        {
                            new ProductCategory
                            {
                                CategoryId =Guid.Parse("AF5251CB-13BA-4D9A-96DF-3A3E32D57DDB")
                            },
                            new ProductCategory
                            {
                                CategoryId =Guid.Parse("C1C2EF6C-110B-4050-9387-8F4060FBD4AA")
                            }
                        }
                    },
                    new Product
                    {
                        Id = Guid.NewGuid() ,
                        AvailableStock = 10 ,
                        Code="ABC1252",
                        Description="Total cleaner bbbbbbbbbb",
                        Price=400.5m,
                        Title="Total cleaner B",
                        ProductCategories = new List<ProductCategory>()
                        {
                            new ProductCategory
                            {
                                CategoryId =Guid.Parse("AF5251CB-13BA-4D9A-96DF-3A3E32D57DDB")
                            },
                            new ProductCategory
                            {
                                CategoryId =Guid.Parse("C1C2EF6C-110B-4050-9387-8F4060FBD4AA")
                            }
                        }
                    },
                    new Product
                    {
                        Id = Guid.NewGuid() ,
                        AvailableStock = 10 ,
                        Code="ABC1253",
                        Description="Total cleaner cccccccc",
                        Price=400.5m,
                        Title="Total cleaner C",
                        ProductCategories = new List<ProductCategory>()
                        {
                            new ProductCategory
                            {
                                CategoryId =Guid.Parse("AF5251CB-13BA-4D9A-96DF-3A3E32D57DDB")
                            },
                            new ProductCategory
                            {
                                CategoryId =Guid.Parse("C1C2EF6C-110B-4050-9387-8F4060FBD4AA")
                            }
                        }
                    }
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
