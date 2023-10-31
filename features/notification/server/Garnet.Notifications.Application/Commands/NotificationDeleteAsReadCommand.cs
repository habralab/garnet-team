using FluentResults;
using Garnet.Common.Application;

namespace Garnet.Notifications.Application.Commands
{
    public class NotificationDeleteAsReadCommand
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ICurrentUserProvider _currentUserProvider;

        public NotificationDeleteAsReadCommand(ICurrentUserProvider currentUserProvider, INotificationRepository notificationRepository)
        {
            _currentUserProvider = currentUserProvider;
            _notificationRepository = notificationRepository;
        }

        public Task<Result<NotificationEntity>> Execute(CancellationToken ct, string notificationId)
        {
            return null;
        }
    }
}