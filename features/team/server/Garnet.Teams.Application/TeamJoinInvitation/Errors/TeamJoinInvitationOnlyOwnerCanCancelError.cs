using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.TeamJoinInvitation.Errors
{
    public class TeamJoinInvitationOnlyOwnerCanCancelError : ApplicationError
    {
        public TeamJoinInvitationOnlyOwnerCanCancelError() : base("Отменять приглашения на вступление в команду может только ее владелец")
        {
        }

        public override string Code => nameof(TeamJoinInvitationOnlyOwnerCanCancelError);
    }
}