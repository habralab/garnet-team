using Garnet.Notifications.Events;

namespace Garnet.Teams.Application.Team.Notifications
{
    public static class TeamEntityNotificationsExtensions
    {
        public static SendNotificationCommandMessage CreateTeamEditOwnerNotification(this TeamEntity team, string username)
        {
            return new SendNotificationCommandMessage(
                Title: "Смена владельца команды",
                Body: $"Владелец команды {team.Name} изменен на пользователя {username}",
                team.OwnerUserId,
                Type: "TeamEditOwner",
                team.AuditInfo.UpdatedAt,
                team.Id
            );
        }
    }
}