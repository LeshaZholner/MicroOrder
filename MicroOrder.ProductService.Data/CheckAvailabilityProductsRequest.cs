namespace MicroOrder.ProductService.Data;

public class CheckAvailabilityProductsRequest
{
    public CheckAvailabilityProductItemRequest[] Products { get; set; } = [];
}
