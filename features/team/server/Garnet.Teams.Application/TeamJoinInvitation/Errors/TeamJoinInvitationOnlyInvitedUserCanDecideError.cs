using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.TeamJoinInvitation.Errors
{
    public class TeamJoinInvitationOnlyInvitedUserCanDecideError : ApplicationError
    {
        public TeamJoinInvitationOnlyInvitedUserCanDecideError() : base("Только приглашенный пользователь может выносить вердикт по своим приглашениям")
        {
        }

        public override string Code => nameof(TeamJoinInvitationOnlyInvitedUserCanDecideError);
    }
}