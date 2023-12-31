using Microsoft.EntityFrameworkCore;
using Sana.Store.Infrastructure;
using System.Reflection;
using Sana.Store.Application.Queries.Catalog;
using MediatR;
using Sana.Store.Entities.Settings;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register MediatR and scan the assembly containing your handlers
builder.Services.AddMediatR(Assembly.GetAssembly(typeof(GetProductsQueryHandler)));

string dbConnectionString = builder.Configuration.GetConnectionString("SanaStore_db")!;

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactCorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000") // Reemplaza con la URL de tu aplicación React
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services
    .AddDbContextPool<ServiceDbContext>(options =>
    {

        options.UseSqlServer(dbConnectionString,
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
        });
    },
        10
    );

builder.Services.AddAutoMapper(typeof(GetProductsQueryHandler).Assembly);

builder.Services.AddSingleton(settings =>
{
    GlobalSettings commonGlobalAppSingleSettings = new GlobalSettings();
    commonGlobalAppSingleSettings.DbConnectionString = dbConnectionString;
    return commonGlobalAppSingleSettings;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

SeedDatabase(app);

app.UseCors("ReactCorsPolicy");

app.Run();


void SeedDatabase(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ServiceDbContext>();
            new ServiceSeeding().SeedAsync(context).Wait();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError("An error occurred while seeding the service database");
            logger.LogError(ex.ToString());
        }
    }
}