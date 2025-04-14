using MicroOrder.OrderService.API.Models;

namespace MicroOrder.OrderService.API.Application.Services;

public interface ICreateOrderService
{
    Task<CreateOrderResponse> CreateOrder(CreateOrderRequest createOrderRequest);
}
