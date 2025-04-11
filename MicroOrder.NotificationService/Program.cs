using MicroOrder.NotificationService;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<NotificationWorker>();

var host = builder.Build();
host.Run();
