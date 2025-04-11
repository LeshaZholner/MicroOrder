namespace MicroOrder.NotificationService.Contracts;

public class OrderCreatedMessage
{
    public string OrderId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
}
