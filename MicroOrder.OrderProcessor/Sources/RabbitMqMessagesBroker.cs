using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace MicroOrder.OrderProcessor.Sources;

public class RabbitMqMessagesBroker<TMessage> : IMessagesBroker<TMessage>
{
    private readonly IProcessorService<TMessage> _processorService;
    private readonly IConnectionFactory _connectionFactory;
    private readonly RabbitMqOptions _rabbitMqOptions;

    public RabbitMqMessagesBroker(IOptions<RabbitMqOptions> options, IProcessorService<TMessage> processorService)
    {
        _rabbitMqOptions = options.Value;
        _processorService = processorService;
        _connectionFactory = new ConnectionFactory
        {
            HostName = _rabbitMqOptions.HostName,
            Port = _rabbitMqOptions.Port,
            UserName = _rabbitMqOptions.UserName,
            Password = _rabbitMqOptions.Password
        };
    }

    public async Task Run(CancellationToken cancellationToken)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclarePassiveAsync(_rabbitMqOptions.OrderCreatedQueue);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (model, arg) =>
        {
            var encodingBody = Encoding.UTF8.GetString(arg.Body.ToArray());
            var value = JsonSerializer.Deserialize<TMessage>(encodingBody);

            if (value == null)
            {
                await channel.BasicRejectAsync(arg.DeliveryTag, false);
                return;
            }

            await _processorService.ProcessMessage(value, CancellationToken.None);

            await channel.BasicAckAsync(arg.DeliveryTag, false);
        };

        await channel.BasicConsumeAsync(_rabbitMqOptions.OrderCreatedQueue, false, consumer);

        var taskCompletionSource = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        await using var _ = cancellationToken.Register(taskCompletionSource.SetResult);
        await taskCompletionSource.Task;

        await channel.CloseAsync();
    }
}
