using System;
using Garnet.Notifications.Events;
using Garnet.Teams.Application.Team;

namespace Garnet.Teams.Application.TeamUserJoinRequest.Notifications
{
    public static class TeamUserJoinRequestEntityNotificationsExtensions
    {
        public static SendNotificationCommandMessage CreateTeamUserJoinRequestNotification(this TeamUserJoinRequestEntity userJoinRequestEntity, TeamEntity team, string username)
        {
            return new SendNotificationCommandMessage(
                Title: "Заявка на вступление в команду",
                Body: $"Пользователь {username} хочет вступить в команду {team.Name}",
                team.OwnerUserId,
                "TeamUserJoinRequest",
                userJoinRequestEntity.AuditInfo.CreatedAt,
                team.Id
            );
        }
    }
}