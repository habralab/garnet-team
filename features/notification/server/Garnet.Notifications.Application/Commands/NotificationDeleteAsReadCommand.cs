using FluentResults;
using Garnet.Common.Application;
using Garnet.Notifications.Application.Errors;

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

        public async Task<Result<NotificationEntity>> Execute(CancellationToken ct, string notificationId)
        {
            var notification = await _notificationRepository.GetNotificationById(ct, notificationId);
            if (notification is null)
            {
                return Result.Fail(new NotificationNotFoundError(notificationId));
            }

            if (notification.UserId != _currentUserProvider.UserId)
            {
                return Result.Fail(new NotificationUserCanNotMarkAsReadError());
            }

            await _notificationRepository.DeleteNotificationById(ct, notificationId);
            return Result.Ok(notification);
        }
    }
}