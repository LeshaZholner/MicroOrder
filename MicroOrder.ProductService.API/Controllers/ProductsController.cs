using MicroOrder.ProductService.API.Infrastructure;
using MicroOrder.ProductService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MicroOrder.ProductService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductDbContext _productDbContext;

    public ProductsController(ProductDbContext productDbContext)
    {
        _productDbContext = productDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productDbContext.Products.ToListAsync();

        var productItemsResponse = products.Select(x => new Product
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price,
            Quantity = x.StockQuantity
        });

        var productsResponse = new GetProductsResponse
        {
            Products = [..productItemsResponse]
        };

        return Ok(productsResponse);
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProduct(Guid productId)
    {
        var product = await _productDbContext.Products.FindAsync(productId);
        
        if (product is null)
        {
            return NotFound();
        }

        var productItemResponse = new GetProductResponse
        {
            Product = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.StockQuantity
            }
        };

        return Ok(productItemResponse);
    }


    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] CreateProductRequest createProductRequest)
    {
        var product = new Infrastructure.Entities.Product
        {
            Name = createProductRequest.Name,
            Price = createProductRequest.Price,
            StockQuantity = createProductRequest.Quantity
        };

        _productDbContext.Products.Add(product);
        await _productDbContext.SaveChangesAsync();

        var createdProductItemResponse = new Product
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.StockQuantity
        };

        var createProductResponse = new GetProductResponse 
        {
            Product = createdProductItemResponse
        };

        return Created($"api/products/{createProductResponse.Product.Id}", createProductResponse);
    }

    [HttpPost]
    [Route("check-availability")]
    public async Task<IActionResult> CheckAvailability(CheckAvailabilityProductsRequest checkAvailabilityProductsRequest)
    {
        var requestedIds = checkAvailabilityProductsRequest.Products.Select(x => x.ProductId).ToList();
        var products = await _productDbContext.Products
            .Where(x => requestedIds.Contains(x.Id))
            .ToListAsync();

        var productItemCheckAvailabilityResponse = checkAvailabilityProductsRequest.Products
            .Join(
                products,
                request => request.ProductId,
                products => products.Id,
                (request, products) => new CheckAvailabilityProductItemResponse
                {
                    Id = products.Id,
                    IsAvailability = products.StockQuantity >= request.Quantity
                }
            );

        var response = new CheckAvailabilityProductsResponse
        {
            Products = [.. productItemCheckAvailabilityResponse]
        };

        return Ok(response);
    }
}
