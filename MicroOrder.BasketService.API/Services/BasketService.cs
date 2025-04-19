namespace MicroOrder.BasketService.API.Services;

public class BasketService : IBasketService
{
    private static readonly string keyPrefix = "basket";
    private readonly IDictionary<string, CustomerBasket> _baskets;

    public BasketService()
    {
        _baskets = new Dictionary<string, CustomerBasket>();
    }

    public Task<CustomerBasket?> GetBasket(Guid customerId)
    {
        _baskets.TryGetValue(GetBasketKey(customerId), out var basket);

        return Task.FromResult(basket);
    }

    public async Task<CustomerBasket> AddItem(Guid customerId, BasketItem item)
    {
        var basket = await GetBasket(customerId)
            ?? new CustomerBasket
            {
                CustomerId = customerId,
                BasketItems = [item]
            };

        return basket;
    }

    public async Task<CustomerBasket> UpdateItemQuantity(Guid customerId, Guid productId, int quantity)
    {
        var basket = await GetBasket(customerId)
            ?? throw new Exception("Basket not found.");

        var basketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId)
            ?? throw new Exception("Basket item not found.");

        basketItem.Quantity = quantity;

        return basket;
    }

    public async Task<CustomerBasket> UpdateBasket(CustomerBasket basket)
    {
        _baskets.Add(GetBasketKey(basket.CustomerId), basket);

        return await GetBasket(basket.CustomerId)
            ?? throw new InvalidOperationException("Basket should not be null after update."); ;
    }

    public async Task RemoveItem(Guid customerId, Guid productId)
    {
        var basket = await GetBasket(customerId)
            ?? throw new Exception("Basket not found.");

        basket.BasketItems.ToList().RemoveAll(b => b.ProductId == productId);
    }

    public Task ClearBasket(Guid customerId)
    {
        _baskets.Remove(GetBasketKey(customerId));

        return Task.CompletedTask;
    }

    private static string GetBasketKey(Guid userId)
    {
        return $"{keyPrefix}_{userId}";
    }
}
