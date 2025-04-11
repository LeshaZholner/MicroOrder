using MicroOrder.API.Models;
using MicroOrder.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroOrder.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IQueueMessagingService _queueMessagingService;

    public OrderController(IQueueMessagingService queueMessagingService)
    {
        _queueMessagingService = queueMessagingService;
    }

    [HttpPost]
    public async Task CreateOrder([FromBody] CreateOrderRequest createOrderRequest)
    {
        await _queueMessagingService.SendMessage(createOrderRequest);
    }
}
