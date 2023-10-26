namespace Garnet.Notifications.Application
{
    public interface INotificationRepository
    {
        Task<NotificationEntity[]> GetNotificationsByUser(CancellationToken ct, string userId);
    }
}