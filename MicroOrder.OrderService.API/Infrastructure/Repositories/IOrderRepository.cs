using MicroOrder.OrderService.API.Infrastructure.Entities;

namespace MicroOrder.OrderService.API.Infrastructure.Repositories;

public interface IOrderRepository
{
    Task AddAsync(Order order);
}
