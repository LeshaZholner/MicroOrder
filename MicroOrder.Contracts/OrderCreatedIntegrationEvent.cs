namespace MicroOrder.Contracts;

public class OrderCreatedIntegrationEvent
{
    public Guid OrderId { get; set; }
    public string Email { get; set; } = string.Empty;
    public OrderCreatedItem[] Items { get; set; } = [];
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
}
