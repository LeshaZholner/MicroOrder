namespace MicroOrder.NotificationService;

public class NotificationWorker : BackgroundService
{
    private readonly ILogger<NotificationWorker> _logger;

    public NotificationWorker(ILogger<NotificationWorker> logger)
    {
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}
