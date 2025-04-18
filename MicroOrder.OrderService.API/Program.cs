using MicroOrder.OrderService.API.Application.Services;
using MicroOrder.OrderService.API.Infrastructure;
using MicroOrder.OrderService.API.Infrastructure.Messaging;
using MicroOrder.OrderService.API.Infrastructure.Repositories;
using MicroOrder.ProductService.Client;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Infrastructure
MongoDbGlobalConventions.RegisterGlobalConventions();
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection(MongoSettings.SectionName));
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});
builder.Services.AddSingleton<OrderDbContext>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Application
builder.Services.AddScoped<ICreateOrderService, CreateOrderService>();

builder.Services.AddProductService(builder.Configuration);

// RabbitMQ
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection(RabbitMqSettings.SectionName));
builder.Services.AddScoped<IQueueMessagingService, RabbitMqQueueMessagingService>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5001";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = "orderserviceapi"
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("OrderServiceScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "orderserviceapi.fullaccess");
    });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
