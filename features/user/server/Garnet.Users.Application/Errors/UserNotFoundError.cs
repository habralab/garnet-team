using Garnet.Common.Application.Errors;

namespace Garnet.Users.Application.Errors
{
    public class UserNotFoundError : ApplicationError
    {
        public UserNotFoundError(string userId) : base($"Пользователь с идентификатором {userId} не найден")
        {
        }

        public override string Code => nameof(UserNotFoundError);
    }
}