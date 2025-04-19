namespace MicroOrder.WebApp.Models.Catalog;

public class CatalogItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
