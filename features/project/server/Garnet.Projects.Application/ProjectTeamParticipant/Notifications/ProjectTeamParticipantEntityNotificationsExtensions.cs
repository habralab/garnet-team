using Garnet.Notifications.Events;
using Garnet.Projects.Application.Project;

namespace Garnet.Projects.Application.ProjectTeamParticipant.Notifications
{
    public static class ProjectTeamParticipantEntityNotificationsExtensions
    {
        public static SendNotificationCommandMessage CreateTeamLeaveProjectNotification(this ProjectTeamParticipantEntity projectTeamParticipant, ProjectEntity project)
        {
            return new SendNotificationCommandMessage(
                Title: "Команда покинула проект",
                Body: $"Команда {projectTeamParticipant.TeamName} покинула проект {project.ProjectName}",
                project.OwnerUserId,
                Type: "TeamLeaveProject",
                DateTimeOffset.Now,
                projectTeamParticipant.TeamId
            );
        }
    }
}