using Garnet.Notifications.Events;
using Garnet.Projects.Application.Project;

namespace Garnet.Projects.Application.ProjectTeamJoinRequest.Notifications
{
    public static class ProjectTeamJoinRequestEntityNotificationsExtensions
    {
        public static SendNotificationCommandMessage CreateProjectTeamJoinRequestNotification(this ProjectTeamJoinRequestEntity projectTeamJoinRequest, ProjectEntity project)
        {
            return new SendNotificationCommandMessage(
                Title: "Заявка на вступление в проект",
                Body: $"Команда {projectTeamJoinRequest.TeamName} хочет вступить в проект {project.ProjectName}",
                project.OwnerUserId,
                Type: "TeamJoinProjectRequest",
                DateTimeOffset.Now,
                projectTeamJoinRequest.TeamId
            );
        }

        public static SendNotificationCommandMessage CreateProjectTeamJoinRequestDecideNotification(this ProjectTeamJoinRequestEntity projectTeamJoinRequest, ProjectEntity project, string teamOwnerUserId, bool isApproved)
        {
            var decision = isApproved ? "принял" : "отклонил";
            return new SendNotificationCommandMessage(
                Title: "Решение по заявке на вступление в проект",
                Body: $"Владелец проекта {project.ProjectName} {decision} заявку на вступление от команды {projectTeamJoinRequest.TeamName}",
                teamOwnerUserId,
                Type: "TeamJoinRequestDecide",
                DateTimeOffset.Now,
                projectTeamJoinRequest.ProjectId
            );
        }
    }
}