using Garnet.Notifications.Application.Args;

namespace Garnet.Notifications.Application
{
    public interface INotificationRepository
    {
        Task<NotificationEntity[]> GetNotificationsByUser(CancellationToken ct, string userId);
        Task CreateNotification(CancellationToken ct, NotificationCreateArgs args);
        Task DeleteNotification(CancellationToken ct, NotificationDeleteArgs args);
    }
}