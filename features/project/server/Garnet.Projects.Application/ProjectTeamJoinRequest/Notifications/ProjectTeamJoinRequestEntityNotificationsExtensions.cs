using Garnet.Notifications.Events;
using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.ProjectTeam;

namespace Garnet.Projects.Application.ProjectTeamJoinRequest.Notifications
{
    public static class ProjectTeamJoinRequestEntityNotificationsExtensions
    {
        public static SendNotificationCommandMessage CreateProjectTeamJoinRequestNotification(this ProjectTeamJoinRequestEntity projectTeamJoinRequest, ProjectEntity project, string teamAvatarUrl)
        {
            var quotes = new NotificationQuotedEntity[]
            {
                new(projectTeamJoinRequest.Id, teamAvatarUrl, projectTeamJoinRequest.TeamName),
                new(project.Id, project.AvatarUrl, project.ProjectName)
            };

            return new SendNotificationCommandMessage(
                Title: "Заявка на вступление в проект",
                Body: $"Команда {projectTeamJoinRequest.TeamName} хочет вступить в проект {project.ProjectName}",
                project.OwnerUserId,
                Type: "TeamJoinProjectRequest",
                DateTimeOffset.Now,
                projectTeamJoinRequest.Id,
                quotes
            );
        }

        public static SendNotificationCommandMessage CreateProjectTeamJoinRequestDecideNotification(this ProjectTeamJoinRequestEntity projectTeamJoinRequest, ProjectEntity project, ProjectTeamEntity team, bool isApproved)
        {
             var quotes = new NotificationQuotedEntity[]
            {
                new(team.Id, team.TeamAvatarUrl!, team.TeamName),
                new(project.Id, project.AvatarUrl, project.ProjectName)
            };

            var decision = isApproved ? "принял" : "отклонил";
            return new SendNotificationCommandMessage(
                Title: "Решение по заявке на вступление в проект",
                Body: $"Владелец проекта {project.ProjectName} {decision} заявку на вступление от команды {projectTeamJoinRequest.TeamName}",
                team.OwnerUserId,
                Type: "TeamJoinRequestDecide",
                DateTimeOffset.Now,
                projectTeamJoinRequest.Id,
                quotes
            );
        }

        public static DeleteNotificationCommandMessage DeleteProjectTeamJoinRequestNotification(this ProjectTeamJoinRequestEntity projectTeamJoinRequest, string projectOwnerUserId)
        {
            return new DeleteNotificationCommandMessage(
                projectOwnerUserId,
                "TeamJoinProjectRequest",
                projectTeamJoinRequest.TeamId
            );
        }
    }
}