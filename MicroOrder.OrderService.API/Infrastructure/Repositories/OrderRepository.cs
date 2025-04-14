using MicroOrder.OrderService.API.Infrastructure.Entities;

namespace MicroOrder.OrderService.API.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _orderDbContext;

    public OrderRepository(OrderDbContext orderDbContext)
    {
        _orderDbContext = orderDbContext;
    }

    public async Task AddAsync(Order order)
    {
        await _orderDbContext.Orders.InsertOneAsync(order);
    }
}
