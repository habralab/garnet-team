using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.TeamJoinInvitation.Errors
{
    public class TeamJoinInvitationOnlyOwnerCanSeeError : ApplicationError
    {
        public TeamJoinInvitationOnlyOwnerCanSeeError() : base("Просматривать приглашения на вступление может только владелец этой команды")
        {
        }

        public override string Code => nameof(TeamJoinInvitationOnlyOwnerCanSeeError);
    }
}