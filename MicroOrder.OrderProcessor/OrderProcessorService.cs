using MicroOrder.OrderProcessor.Contracts;
using MicroOrder.OrderProcessor.Objects;
using MicroOrder.OrderProcessor.Services;

namespace MicroOrder.OrderProcessor;

public class OrderProcessorService : IProcessorService<Order>
{
    private readonly ICreateOrderService _createOrderService;

    public OrderProcessorService(ICreateOrderService createOrderService)
    {
        _createOrderService = createOrderService;
    }

    public async Task ProcessMessage(Order message, CancellationToken cancellationToken)
    {
        var createOrderCommand = new CreateOrderCommand
        {
            Email = message.Email,
            Products = [.. message.Items.Select(x => new CreateOrderProductLineModel
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
            })]
        };

        await _createOrderService.Create(createOrderCommand, cancellationToken);
    }
}
