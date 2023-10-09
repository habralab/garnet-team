using Garnet.Common.Application.Errors;

namespace Garnet.Teams.Application.TeamJoinInvitation.Errors
{
    public class TeamPendingJoinInvitationError : ApplicationError
    {
        public TeamPendingJoinInvitationError(string userId) : base($"Пользователю с идентификатором '{userId}' уже было направлено приглашение в команду")
        {
        }

        public override string Code => nameof(TeamPendingJoinInvitationError);
    }
}