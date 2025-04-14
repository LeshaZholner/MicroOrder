namespace MicroOrder.OrderService.API.Models;

public class CreateOrderResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public CreateOrderLineResponse[] Products { get; set; } = [];
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
}
