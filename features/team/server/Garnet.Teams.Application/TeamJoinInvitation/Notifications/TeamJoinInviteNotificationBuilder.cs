using Garnet.Notifications.Events;
using Garnet.Teams.Application.Team;

namespace Garnet.Teams.Application.TeamJoinInvitation.Notifications
{
    public static class TeamJoinInviteNotificationBuilder
    {
        public static SendNotificationCommandMessage CreateTeamInviteNotification(this TeamJoinInvitationEntity invitation, TeamEntity team)
        {
            return new SendNotificationCommandMessage(
                Title: "Приглашение в команду",
                Body: $"Вас пригласили вступить в команду {team.Name}",
                invitation.UserId,
                Type: "TeamInvite",
                invitation.AuditInfo.CreatedAt,
                team.Id
            );
        }
    }
}