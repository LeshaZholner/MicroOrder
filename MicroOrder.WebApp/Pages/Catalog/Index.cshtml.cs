using MicroOrder.ProductService.Client;
using MicroOrder.WebApp.Models.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroOrder.WebApp.Pages.Catalog;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IProductService _productService;

    public IndexModel(IProductService productService)
    {
        _productService = productService;
    }

    public CatalogItem[] CatalogItems { get; set; } = [];

    public async Task<IActionResult> OnGet()
    {
        var productsResponse = await _productService.GetProducts(CancellationToken.None);

        CatalogItems = [.. productsResponse.Products.Select(x => new CatalogItem
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price,
        })];

        return Page();
    }
}
