using MicroOrder.BasketService.Contracts;

namespace MicroOrder.BasketService.Client;

public interface IBasketServiceClient
{
    Task<CustomerBasketResponse?> GetBasket();
    Task<CustomerBasketResponse?> AddItem(AddBasketItemRequest basketItem);
}
