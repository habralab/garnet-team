using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Errors
{
    public class TeamUserNotATeamParticipantError : ApplicationError
    {
        public TeamUserNotATeamParticipantError(string userId) : base($"Пользователь с идентификатором '{userId}' не является участником команды")
        {
        }

        public override string Code => nameof(TeamUserNotATeamParticipantError);
    }
}