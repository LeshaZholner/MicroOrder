using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace MicroOrder.OrderService.API.Infrastructure.Messaging;

public class RabbitMqQueueMessagingService : IQueueMessagingService
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly RabbitMqSettings _rabbitMqSettings;

    public RabbitMqQueueMessagingService(IOptions<RabbitMqSettings> rabbitMqSettings)
    {
        _rabbitMqSettings = rabbitMqSettings.Value;
        _connectionFactory = new ConnectionFactory
        {
            HostName = _rabbitMqSettings.HostName,
            Port = _rabbitMqSettings.Port,
            UserName = _rabbitMqSettings.UserName,
            Password = _rabbitMqSettings.Password,
        };
    }

    public async Task PublishAsync<TMessage>(TMessage message)
    {
        try
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclarePassiveAsync(_rabbitMqSettings.Exchange);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            var properties = new BasicProperties
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };

            await channel.BasicPublishAsync(
                _rabbitMqSettings.Exchange,
                _rabbitMqSettings.CreatedRoutingKey,
                true, properties,
                body);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
