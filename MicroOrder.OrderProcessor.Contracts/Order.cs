namespace MicroOrder.OrderProcessor.Contracts;

public class Order
{
    public string Email { get; set; } = string.Empty;
    public OrderItem[] Items { get; set; } = [];
}
