using Garnet.Common.Application.Errors;

namespace Garnet.Notifications.Application.Errors
{
    public class NotificationNotFoundError : ApplicationError
    {
        public NotificationNotFoundError(string notificationId) : base($"Уведомление с идентификатором {notificationId} не найдено")
        {
        }

        public override string Code => nameof(NotificationNotFoundError);
    }
}