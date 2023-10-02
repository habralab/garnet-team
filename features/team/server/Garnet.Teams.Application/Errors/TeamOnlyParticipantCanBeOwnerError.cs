using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Errors
{
    public class TeamOnlyParticipantCanBeOwnerError : ApplicationError
    {
        public TeamOnlyParticipantCanBeOwnerError(string userId) : base($"Пользователь с идентификатором '{userId}' не является участником команды")
        {
        }

        public override string Code => nameof(TeamOnlyParticipantCanBeOwnerError);
    }
}