﻿namespace MicroOrder.WebAppNew.Components.Pages.Product;

public class ProductItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
