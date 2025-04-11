namespace MicroOrder.API.Models;

public class CreateOrderRequest
{
    public string Email { get; set; } = string.Empty;
    public OrderItemCreateOrderRequest[] Items { get; set; } = [];
}
