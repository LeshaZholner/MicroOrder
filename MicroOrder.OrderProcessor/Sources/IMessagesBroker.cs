namespace MicroOrder.OrderProcessor.Sources;

public interface IMessagesBroker<TMessage>
{
    Task Run(CancellationToken cancellationToken);
}
