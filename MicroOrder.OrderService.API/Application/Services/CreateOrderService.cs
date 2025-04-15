using MicroOrder.Contracts;
using MicroOrder.OrderService.API.Infrastructure.Entities;
using MicroOrder.OrderService.API.Infrastructure.Messaging;
using MicroOrder.OrderService.API.Infrastructure.Repositories;
using MicroOrder.OrderService.API.Models;
using MicroOrder.ProductService.Client;

namespace MicroOrder.OrderService.API.Application.Services;

public class CreateOrderService : ICreateOrderService
{
    private readonly IProductService _productService;
    private readonly IOrderRepository _orderRepository;
    private readonly IQueueMessagingService _queueMessagingService;

    public CreateOrderService(
        IProductService productService,
        IOrderRepository orderRepository,
        IQueueMessagingService queueMessagingService)
    {
        _productService = productService;
        _orderRepository = orderRepository;
        _queueMessagingService = queueMessagingService;
    }

    public async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest createOrderRequest)
    {
        var order = await BuildOrderAsync(createOrderRequest);

        await _orderRepository.AddAsync(order);
        await AddAndSaveEventAsync(order);

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

    private async Task<Order> BuildOrderAsync(CreateOrderRequest createOrderRequest)
    {
        var products = await _productService.GetProducts(CancellationToken.None);

        var productIds = new HashSet<Guid>(createOrderRequest.Items.Select(x => x.ProductId));
        var orderItems = products.Products.Where(x => productIds.Contains(x.Id))
            .Select(x => new OrderItem
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Quantity = createOrderRequest.Items.First(y => y.ProductId == x.Id).Quantity,
            });

        return new Order
        {
            Email = createOrderRequest.Email,
            OrderItems = [.. orderItems],
            CreatedAt = DateTime.UtcNow,
        };
    }

    private async Task AddAndSaveEventAsync(Order order)
    {
        var @event = new OrderCreatedIntegrationEvent
        {
            OrderId = order.Id,
            Email = order.Email,
            Items = [.. order.OrderItems.Select(x => new OrderCreatedItem {
                ProductId = x.Id,
                Name = x.Name,
                Quantity = x.Quantity,
                Price = x.Price,
            })],
            TotalAmount = order.TotalAmount,
            CreatedAt = order.CreatedAt,
        };

        await _queueMessagingService.PublishAsync(@event);
    }
}
