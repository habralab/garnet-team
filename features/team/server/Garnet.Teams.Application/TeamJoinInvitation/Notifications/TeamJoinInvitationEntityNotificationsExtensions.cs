using Garnet.Notifications.Events;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.TeamUser;

namespace Garnet.Teams.Application.TeamJoinInvitation.Notifications
{
    public static class TeamJoinInvitationEntityNotificationsExtensions
    {
        public static SendNotificationCommandMessage CreateTeamInviteNotification(this TeamJoinInvitationEntity invitation, TeamEntity team)
        {
            var quotes = new NotificationQuotedEntity[]
            {
                new(team.Id, team.AvatarUrl, team.Name)
            };

            return new SendNotificationCommandMessage(
                Title: "Приглашение в команду",
                Body: $"Вас пригласили вступить в команду {team.Name}",
                invitation.UserId,
                Type: "TeamInvite",
                invitation.AuditInfo.CreatedAt,
                invitation.Id,
                quotes
            );
        }


        public static DeleteNotificationCommandMessage DeleteTeamInviteNotification(this TeamJoinInvitationEntity invitation)
        {
            return new DeleteNotificationCommandMessage(
               invitation.UserId,
               Type: "TeamInvite",
               invitation.TeamId
           );
        }

        public static SendNotificationCommandMessage CreateTeamInviteDecideNotification(this TeamJoinInvitationEntity invitation, TeamEntity team, TeamUserEntity user, bool isApproved)
        {
            var quotes = new NotificationQuotedEntity[]
            {
                new(team.Id, team.AvatarUrl, team.Name),
                new(user.Id, user.AvatarUrl, user.Username)
            };

            var decision = isApproved ? "принял" : "отклонил";
            return new SendNotificationCommandMessage(
                Title: "Решение по приглашению в команду",
                Body: $"Пользователь {user.Username} {decision} приглашение на вступление в команду {team.Name}",
                team.OwnerUserId,
                Type: "TeamInviteDecide",
                DateTimeOffset.Now,
                invitation.Id,
                quotes
            );
        }
    }
}