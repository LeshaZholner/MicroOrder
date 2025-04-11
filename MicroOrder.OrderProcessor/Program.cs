using MicroOrder.OrderProcessor;
using MicroOrder.OrderProcessor.Contracts;
using MicroOrder.OrderProcessor.Services;
using MicroOrder.OrderProcessor.Sources;
using MicroOrder.ProductService.Client;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection(RabbitMqOptions.SectionName));
builder.Services.AddProductService(builder.Configuration);
builder.Services.AddTransient<ICreateOrderService, CreateOrderService>();
builder.Services.AddTransient<IProcessorService<Order>, OrderProcessorService>();
builder.Services.AddTransient<IMessagesBroker<Order>, RabbitMqMessagesBroker<Order>>();
builder.Services.AddHostedService<BackgroundProcessorService<Order>>();

//builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
