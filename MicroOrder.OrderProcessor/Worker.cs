using MicroOrder.ProductService.Client;
using System.Text.Json;

namespace MicroOrder.OrderProcessor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IProductService _productService;

        public Worker(ILogger<Worker> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                var products = await _productService.GetProducts(stoppingToken);

                _logger.LogInformation(JsonSerializer.Serialize(products));

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
