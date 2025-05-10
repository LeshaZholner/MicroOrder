namespace MicroOrder.WebAppNew.Components.Pages.Catalog;

public class CatalogItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Url { get; set; } = string.Empty;
}
