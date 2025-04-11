namespace MicroOrder.OrderProcessor.Objects;

public class CreateOrderProductLineModel
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
