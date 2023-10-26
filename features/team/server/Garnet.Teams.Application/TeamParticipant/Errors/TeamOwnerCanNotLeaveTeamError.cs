using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.TeamParticipant.Errors
{
    public class TeamOwnerCanNotLeaveTeamError : ApplicationError
    {
        public TeamOwnerCanNotLeaveTeamError() : base("Владелец команды не может выйти из ее состава")
        {
        }

        public override string Code => nameof(TeamOwnerCanNotLeaveTeamError);
    }
}