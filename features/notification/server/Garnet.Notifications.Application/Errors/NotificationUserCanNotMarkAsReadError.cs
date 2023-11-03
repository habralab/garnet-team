using Garnet.Common.Application.Errors;

namespace Garnet.Notifications.Application.Errors
{
    public class NotificationUserCanNotMarkAsReadError : ApplicationError
    {
        public NotificationUserCanNotMarkAsReadError() : base("Пользователь может отмечать как прочитанное только свои уведомления")
        {
        }

        public override string Code => nameof(NotificationUserCanNotMarkAsReadError);
    }
}