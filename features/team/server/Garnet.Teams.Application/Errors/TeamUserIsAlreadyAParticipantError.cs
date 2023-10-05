using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Errors
{
    public class TeamUserIsAlreadyAParticipantError : ApplicationError
    {
        public TeamUserIsAlreadyAParticipantError(string userId) : base($"Пользователь с идентификатором '{userId}' уже участник команды")
        {
        }

        public override string Code => nameof(TeamUserIsAlreadyAParticipantError);
    }
}