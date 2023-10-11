using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.TeamJoinProjectRequest.Errors
{
    public class TeamOnlyOwnerCanRequestJoiningProjectError : ApplicationError
    {
        public TeamOnlyOwnerCanRequestJoiningProjectError() : base("Только владелец команды может подавать заявки на участие в проектах")
        {
        }

        public override string Code => nameof(TeamOnlyOwnerCanRequestJoiningProjectError);
    }
}