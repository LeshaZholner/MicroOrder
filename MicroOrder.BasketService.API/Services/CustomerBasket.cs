namespace MicroOrder.BasketService.API.Services;

public class CustomerBasket
{
    public Guid CustomerId { get; set; }
    public BasketItem[] BasketItems { get; set; } = [];
}
