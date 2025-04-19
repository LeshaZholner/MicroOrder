namespace MicroOrder.BasketService.API.Models;

public class UpdateBasketRequest
{
    public UpdateBasketItemRequest[] Items { get; set; } = [];
}
