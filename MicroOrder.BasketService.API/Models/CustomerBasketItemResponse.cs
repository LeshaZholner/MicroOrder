﻿namespace MicroOrder.BasketService.API.Models;

public class CustomerBasketItemResponse
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
