using Garnet.Notifications.Application.Args;

namespace Garnet.Notifications.Application.Commands
{
    public class NotificationDeleteCommand
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationDeleteCommand(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task Execute(CancellationToken ct, NotificationDeleteArgs args)
        {
            await _notificationRepository.DeleteNotification(ct, args);
        }
    }
}