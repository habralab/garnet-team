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

        public Task Execute(CancellationToken ct, NotificationDeleteArgs args)
        {
            return Task.CompletedTask;
        }
    }
}