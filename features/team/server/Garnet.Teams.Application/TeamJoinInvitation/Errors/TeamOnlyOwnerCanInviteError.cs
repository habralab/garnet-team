using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.TeamJoinInvitation.Errors
{
    public class TeamOnlyOwnerCanInviteError : ApplicationError
    {
        public TeamOnlyOwnerCanInviteError() : base("Приглашать пользователей в команду может только владелец этой команды")
        {
        }

        public override string Code => nameof(TeamOnlyOwnerCanInviteError);
    }
}