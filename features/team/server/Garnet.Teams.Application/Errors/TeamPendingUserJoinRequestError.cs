using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Errors
{
    public class TeamPendingUserJoinRequestError : ApplicationError
    {
        public TeamPendingUserJoinRequestError(string userId) : base($"Пользователь с идентификатором '{userId}' уже направил заявку на вступление в команду")
        {
        }

        public override string Code => nameof(TeamPendingUserJoinRequestError);
    }
}