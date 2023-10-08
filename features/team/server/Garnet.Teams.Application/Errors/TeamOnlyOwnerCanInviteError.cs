using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.Errors
{
    public class TeamOnlyOwnerCanInviteError : ApplicationError
    {
        public TeamOnlyOwnerCanInviteError() : base("Приглашать пользователей в команду может только владелец этой команды")
        {
        }

        public override string Code => nameof(TeamOnlyOwnerCanInviteError);
    }
}