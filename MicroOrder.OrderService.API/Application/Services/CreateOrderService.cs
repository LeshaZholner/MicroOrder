using MicroOrder.OrderService.API.Infrastructure.Entities;
using MicroOrder.OrderService.API.Infrastructure.Repositories;
using MicroOrder.OrderService.API.Models;
using MicroOrder.ProductService.Client;

namespace MicroOrder.OrderService.API.Application.Services;

public class CreateOrderService : ICreateOrderService
{
    private readonly IProductService _productService;
    private readonly IOrderRepository _orderRepository;

    public CreateOrderService(IProductService productService, IOrderRepository orderRepository)
    {
        _productService = productService;
        _orderRepository = orderRepository;
    }

    public async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest createOrderRequest)
    {
        var products = await _productService.GetProducts(CancellationToken.None);

        var productIds = new HashSet<Guid>(createOrderRequest.Items.Select(x => x.ProductId));
        var orderItems = products.Products.Where(x => productIds.Contains(x.Id))
            .Select(x => new OrderItem { 
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Quantity = createOrderRequest.Items.First(y => y.ProductId == x.Id).Quantity,
            });

        var order = new Order
        {
            Id = Guid.NewGuid(),
            Email = createOrderRequest.Email,
            OrderItems = [.. orderItems],
            CreatedAt = DateTime.Now,
        };

        await _orderRepository.AddAsync(order);

        return new CreateOrderResponse
        {
            Id = order.Id,
            Email = order.Email,
            Products = [.. order.OrderItems.Select(x => new CreateOrderLineResponse
            {
                ProductId = x.Id,
                Name = x.Name,
                Quantity = x.Quantity,
                Price = x.Price
            })],
            TotalAmount = order.TotalAmount,
            CreatedAt = order.CreatedAt,
        };
    }
}
