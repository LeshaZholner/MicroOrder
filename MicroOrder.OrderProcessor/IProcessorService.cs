namespace MicroOrder.OrderProcessor;

public interface IProcessorService<TMessage>
{
    Task ProcessMessage(TMessage message, CancellationToken cancellationToken);
}
