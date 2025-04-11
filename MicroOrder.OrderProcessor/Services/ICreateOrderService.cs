using MicroOrder.OrderProcessor.Objects;

namespace MicroOrder.OrderProcessor.Services;

public interface ICreateOrderService
{
    Task Create(CreateOrderCommand createOrderCommand, CancellationToken cancellationToken);
}
