using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.TeamJoinInvitation.Errors
{
    public class TeamJoinInvitationNotFoundError : ApplicationError
    {
        public TeamJoinInvitationNotFoundError(string id) : base($"Приглашение с идентификатором {id} на вступление в команду не найдено")
        {
        }

        public override string Code => nameof(TeamJoinInvitationNotFoundError);
    }
}