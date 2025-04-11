namespace MicroOrder.OrderProcessor.Objects;

public class CreateOrderCommand
{
    public string Email { get; set; } = string.Empty;
    public CreateOrderProductLineModel[] Products { get; set; } = [];
}
