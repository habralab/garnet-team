using Garnet.Notifications.Events;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.TeamUser;

namespace Garnet.Teams.Application.TeamUserJoinRequest.Notifications
{
    public static class TeamUserJoinRequestEntityNotificationsExtensions
    {
        public static SendNotificationCommandMessage CreateTeamUserJoinRequestNotification(this TeamUserJoinRequestEntity userJoinRequestEntity, TeamEntity team, TeamUserEntity user)
        {
            var quotes = new NotificationQuotedEntity[]
            {
                new(team.Id, team.AvatarUrl, team.Name),
                new(user.Id, user.AvatarUrl, user.Username)
            };

            return new SendNotificationCommandMessage(
                Title: "Заявка на вступление в команду",
                Body: $"Пользователь {user.Username} хочет вступить в команду {team.Name}",
                team.OwnerUserId,
                "TeamUserJoinRequest",
                userJoinRequestEntity.AuditInfo.CreatedAt,
                userJoinRequestEntity.Id,
                quotes
            );
        }

        public static DeleteNotificationCommandMessage DeleteTeamUserJoinRequestNotification(this TeamUserJoinRequestEntity userJoinRequestEntity, string teamOwnerUserId)
        {
            return new DeleteNotificationCommandMessage(
               teamOwnerUserId,
               Type: "TeamUserJoinRequest",
               userJoinRequestEntity.UserId
           );
        }

        public static SendNotificationCommandMessage CreateTeamUserJoinRequestDecideNotification(this TeamUserJoinRequestEntity userJoinRequestEntity, TeamEntity team, bool isApproved)
        {
            var quotes = new NotificationQuotedEntity[]
            {
                new(team.Id, team.AvatarUrl, team.Name)
            };

            var decision = isApproved ? "принял" : "отклонил";
            return new SendNotificationCommandMessage(
                Title: "Решение по заявке на вступление в команду",
                Body: $"Владелец команды {team.Name} {decision} вашу заявку на вступление",
                userJoinRequestEntity.UserId,
                "TeamUserJoinRequestDecide",
                DateTimeOffset.Now,
                userJoinRequestEntity.Id,
                quotes
            );
        }
    }
}