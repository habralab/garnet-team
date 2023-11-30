using Garnet.Notifications.Events;
using Garnet.Teams.Application.Team;

namespace Garnet.Teams.Application.TeamParticipant.Notifications
{
    public static class TeamParticipantEntityNotificationsExtensions
    {
        public static SendNotificationCommandMessage CreateParticipantLeaveTeamNotification(this TeamParticipantEntity participant, TeamEntity team, string username)
        {
             var quotes = new NotificationQuotedEntity[]
            {
                new(team.Id, team.AvatarUrl, team.Name),
                new(participant.UserId, team.AvatarUrl, team.Name)
            };

            return new SendNotificationCommandMessage(
                Title: "Участник покинул команду",
                Body: $"Пользователь {username} вышел из состава команды {team.Name}",
                team.OwnerUserId,
                Type: "TeamParticipantLeaveTeam",
                DateTimeOffset.Now,
                participant.Id,
                quotes
            );
        }
    }
}