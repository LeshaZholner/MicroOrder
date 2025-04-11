using MicroOrder.API.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MicroOrder.API.Services;

public class RabbitMqQueueMessagingService : IQueueMessagingService
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly RabbitMqOptions _rabbitMqSettings;

    public RabbitMqQueueMessagingService(IOptions<RabbitMqOptions> settings)
    {
        _rabbitMqSettings = settings.Value;

        _connectionFactory = new ConnectionFactory
        {
            HostName = _rabbitMqSettings.HostName,
            Port = _rabbitMqSettings.Port,
            UserName = _rabbitMqSettings.UserName,
            Password = _rabbitMqSettings.Password,
        };
    }

    public async Task SendMessage<TMessage>(TMessage message)
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
