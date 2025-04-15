using MicroOrder.ProductService.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var productConnectionString = builder.Configuration.GetConnectionString("MicroOrderProductConnectionString");
builder.Services.AddDbContext<ProductDbContext>(options =>
{
    options.UseSqlServer(productConnectionString);
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


/*
 
 dotnet ef migrations add AddProduct -s .\MicroOrder.ProductService.API -p .\MicroOrder.ProductService.API --output-dir .\Infrastructure\Migrations
 dotnet ef database update -s .\MicroOrder.ProductService.API -p .\MicroOrder.ProductService.API
 */