namespace MicroOrder.OrderProcessor.Contracts;

public class OrderItem
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
