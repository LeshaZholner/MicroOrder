namespace MicroOrder.OrderService.API.Infrastructure.Messaging;

public class RabbitMqSettings
{
    public const string SectionName = "RabbitMqSettings";
    public string HostName { get; set; } = string.Empty;
    public int Port { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Exchange { get; set; } = string.Empty;
    public string CreatedRoutingKey { get; set; } = string.Empty;
}
