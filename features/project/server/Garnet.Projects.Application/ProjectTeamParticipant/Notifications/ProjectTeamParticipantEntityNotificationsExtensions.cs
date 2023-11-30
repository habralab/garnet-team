using Garnet.Notifications.Events;
using Garnet.Projects.Application.Project;

namespace Garnet.Projects.Application.ProjectTeamParticipant.Notifications
{
    public static class ProjectTeamParticipantEntityNotificationsExtensions
    {
        public static SendNotificationCommandMessage CreateTeamLeaveProjectNotification(this ProjectTeamParticipantEntity projectTeamParticipant, ProjectEntity project)
        {
            var quotes = new NotificationQuotedEntity[]
            {
                new(projectTeamParticipant.TeamId, projectTeamParticipant.TeamAvatarUrl!, projectTeamParticipant.TeamName),
                new(project.Id, project.AvatarUrl, project.ProjectName)
            };

            return new SendNotificationCommandMessage(
                Title: "Команда покинула проект",
                Body: $"Команда {projectTeamParticipant.TeamName} покинула проект {project.ProjectName}",
                project.OwnerUserId,
                Type: "TeamLeaveProject",
                DateTimeOffset.Now,
                projectTeamParticipant.Id,
                quotes
            );
        }
    }
}