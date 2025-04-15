namespace MicroOrder.OrderService.API.Infrastructure.Messaging;

public interface IQueueMessagingService
{
    Task PublishAsync<TMessage>(TMessage message);
}
