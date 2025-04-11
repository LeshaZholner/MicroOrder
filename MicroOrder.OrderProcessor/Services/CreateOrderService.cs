using MicroOrder.OrderProcessor.Objects;
using MicroOrder.ProductService.Client;

namespace MicroOrder.OrderProcessor.Services;

public class CreateOrderService : ICreateOrderService
{
    private readonly IProductService _productService;

    public CreateOrderService(IProductService productService)
    {
        _productService = productService;
    }

    public Task Create(CreateOrderCommand createOrderCommand, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
