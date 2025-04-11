using MicroOrder.OrderProcessor.Sources;

namespace MicroOrder.OrderProcessor;

public class BackgroundProcessorService<TMessage> : BackgroundService
{
    private readonly IMessagesBroker<TMessage> _messagesBroker;
    private readonly ILogger<BackgroundProcessorService<TMessage>> _logger;

    public BackgroundProcessorService(ILogger<BackgroundProcessorService<TMessage>> logger, IMessagesBroker<TMessage> messagesBroker)
    {
        _logger = logger;
        _messagesBroker = messagesBroker;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await _messagesBroker.Run(stoppingToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}
