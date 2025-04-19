namespace MictoOrder.BasketService.API.Services;

public interface IBasketService
{
    Task<CustomerBasket?> GetBasket(Guid customerId);
    Task<CustomerBasket> AddItem(Guid customerId, BasketItem item);
    Task<CustomerBasket> UpdateItemQuantity(Guid customerId, Guid productId, int quantity);
    Task<CustomerBasket> UpdateBasket(CustomerBasket basket);
    Task RemoveItem(Guid customerId, Guid productId);
    Task ClearBasket(Guid customerId);
}
