using MicroOrder.OrderService.API.Application.Services;
using MicroOrder.OrderService.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroOrder.OrderService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ICreateOrderService _createOrderService;

    public OrderController(ICreateOrderService createOrderService)
    {
        _createOrderService = createOrderService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest createOrderRequest)
    {
        var newOrder = await _createOrderService.CreateOrder(createOrderRequest);

        return Ok(newOrder);
    }
}
