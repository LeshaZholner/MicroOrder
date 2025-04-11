namespace MicroOrder.ProductService.Data;

public class CheckAvailabilityProductItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
