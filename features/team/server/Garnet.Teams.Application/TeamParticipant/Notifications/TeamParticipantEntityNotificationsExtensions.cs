using Garnet.Notifications.Events;
using Garnet.Teams.Application.Team;

namespace Garnet.Teams.Application.TeamParticipant.Notifications
{
    public static class TeamParticipantEntityNotificationsExtensions
    {
        public static SendNotificationCommandMessage CreateParticipantLeaveTeamNotification(this TeamParticipantEntity participant, TeamEntity team, string username)
        {
            return new SendNotificationCommandMessage(
                Title: "Участник покинул команду",
                Body: $"Пользователь {username} вышел из состава команды {team.Name}",
                team.OwnerUserId,
                Type: "TeamParticipantLeaveTeam",
                DateTimeOffset.Now,
                participant.UserId
            );
        }
    }
}