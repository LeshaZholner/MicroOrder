﻿namespace MicroOrder.BasketService.Contracts;

public class AddBasketItemRequest
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
