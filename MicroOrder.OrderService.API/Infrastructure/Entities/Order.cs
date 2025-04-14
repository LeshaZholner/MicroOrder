namespace MicroOrder.OrderService.API.Infrastructure.Entities;

public class Order
{
    public Guid Id {  get; set; }
    public string Email { get; set; } = string.Empty;
    public OrderItem[] OrderItems { get; set; } = [];
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
}
