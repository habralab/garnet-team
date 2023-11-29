using Garnet.Notifications.Events;
using Garnet.Teams.Application.TeamUser;

namespace Garnet.Teams.Application.Team.Notifications
{
    public static class TeamEntityNotificationsExtensions
    {
        public static SendNotificationCommandMessage CreateTeamEditOwnerNotification(this TeamEntity team, TeamUserEntity user)
        {
            var quotes = new NotificationQuotedEntity[]
            {
                new(team.Id, team.AvatarUrl, team.Name),
                new(user.Id, user.AvatarUrl, user.Username)
            };

            return new SendNotificationCommandMessage(
                Title: "Смена владельца команды",
                Body: $"Владелец команды {team.Name} изменен на пользователя {user.Username}",
                team.OwnerUserId,
                Type: "TeamEditOwner",
                team.AuditInfo.UpdatedAt,
                team.Id,
                quotes
            );
        }
    }
}