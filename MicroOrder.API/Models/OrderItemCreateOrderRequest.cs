namespace MicroOrder.API.Models;

public class OrderItemCreateOrderRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
