namespace MicroOrder.NotificationService.Services;

public interface ICreateOrderNotificationService
{
    Task SendEmail();
}
