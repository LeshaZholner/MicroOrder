using Microsoft.AspNetCore.Mvc;
using MictoOrder.BasketService.API.Models;
using MictoOrder.BasketService.API.Services;

namespace MictoOrder.BasketService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<IActionResult> GetBasket()
    {
        var basket = await _basketService.GetBasket(Guid.NewGuid());

        if (basket == null)
        {
            return NotFound();
        }

        var basketResponse = MapToCustomerBasketResponse(basket);

        return Ok(basketResponse);
    }

    public async Task<IActionResult> AddBasketItem(AddBasketItemRequest newBasketItem)
    {
        var basketItem = new BasketItem
        {
            ProductId = newBasketItem.ProductId,
            Quantity = newBasketItem.Quantity,
            UnitPrice = newBasketItem.Price
        };

        var basket = await _basketService.AddItem(Guid.NewGuid(), basketItem);

        var basketResponse = MapToCustomerBasketResponse(basket);

        return Ok(basketResponse);
    }

    public async Task<IActionResult> UpdateBasket(UpdateBasketRequest updateBasketRequest)
    {
        var customerBasket = MapToCustomerBasket(Guid.NewGuid(), updateBasketRequest);

        var basket = await _basketService.UpdateBasket(customerBasket);

        if (basket == null)
        {
            return NotFound();
        }

        var basketResponse = MapToCustomerBasketResponse(basket);

        return Ok(basketResponse);
    }

    public async Task ClearBasket()
    {
        await _basketService.ClearBasket(Guid.NewGuid());
    }

    private static CustomerBasketResponse MapToCustomerBasketResponse(CustomerBasket customerBasket)
    {
        return new CustomerBasketResponse
        {
            Items = [.. customerBasket.BasketItems.Select(x => new CustomerBasketItemResponse
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Price = x.UnitPrice,
            })],
        };
    }

    private static CustomerBasket MapToCustomerBasket(Guid customerId, UpdateBasketRequest updateBasketRequest)
    {
        return new CustomerBasket
        {
            CustomerId = customerId,
            BasketItems = [.. updateBasketRequest.Items.Select(x => new BasketItem
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.Price,
            })],
        };
    }
}
