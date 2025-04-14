namespace MicroOrder.OrderService.API.Models;

public class CreateOrderRequest
{
    public string Email { get; set; } = string.Empty;
    public CreateOrderItemDto[] Items { get; set; } = [];
}
