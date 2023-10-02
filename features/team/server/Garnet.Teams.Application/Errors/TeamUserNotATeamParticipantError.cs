using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Errors
{
    public class TeamUserNotATeamParticipantError : ApplicationError
    {
        public TeamUserNotATeamParticipantError() : base("Изменить владельца команды может только ее владелец")
        {
        }

        public override string Code => nameof(TeamUserNotATeamParticipantError);
    }
}