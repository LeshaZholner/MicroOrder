namespace MicroOrder.API.Services;

public interface IQueueMessagingService
{
    Task SendMessage<TMessage>(TMessage message);
}
