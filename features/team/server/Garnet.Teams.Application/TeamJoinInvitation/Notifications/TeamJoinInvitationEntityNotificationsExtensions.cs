using Garnet.Notifications.Events;
using Garnet.Teams.Application.Team;

namespace Garnet.Teams.Application.TeamJoinInvitation.Notifications
{
    public static class TeamJoinInvitationEntityNotificationsExtensions
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


        public static DeleteNotificationCommandMessage DeleteTeamInviteNotification(this TeamJoinInvitationEntity invitation)
        {
            return new DeleteNotificationCommandMessage(
               invitation.UserId,
               Type: "TeamJoinInvitation",
               invitation.TeamId
           );
        }

        public static SendNotificationCommandMessage CreateTeamInviteDecideNotification(this TeamJoinInvitationEntity invitation, TeamEntity team, string username, bool isApproved)
        {
            var decision = isApproved ? "принял" : "отклонил";
            return new SendNotificationCommandMessage(
                Title: "Решение по приглашению в команду",
                Body: $"Пользователь {username} {decision} приглашение на вступление в команду {team.Name}",
                team.OwnerUserId,
                Type: "TeamInviteDecide",
                DateTimeOffset.Now,
                invitation.UserId
            );
        }
    }
}