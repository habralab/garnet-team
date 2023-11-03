using Garnet.Notifications.Events;

namespace Garnet.Projects.Application.Project.Notifications
{
    public static class ProjectEntityNotificationsExtensions
    {
        public static SendNotificationCommandMessage CreateProjectEditOwnerNotification(this ProjectEntity project, string username)
        {
            return new SendNotificationCommandMessage(
                Title: "Смена владельца проекта",
                Body: $"Владелец проекта {project.ProjectName} изменен на пользователя {username}",
                project.OwnerUserId,
                Type: "ProjectEditOwner",
                project.AuditInfo.UpdatedAt,
                project.Id
            );
        }
    }
}