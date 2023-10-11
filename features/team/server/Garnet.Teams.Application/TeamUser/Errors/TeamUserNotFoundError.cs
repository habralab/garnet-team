using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.TeamUser.Errors
{
    public class TeamUserNotFoundError : ApplicationError
    {
        public TeamUserNotFoundError(string userId) : base($"Пользователь с идентификатором '{userId}' не найден")
        {
        }

        public override string Code => nameof(TeamUserNotFoundError);
    }
}