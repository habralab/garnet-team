using Garnet.Notifications.Events;
using Garnet.Projects.Application.ProjectUser;

namespace Garnet.Projects.Application.Project.Notifications
{
    public static class ProjectEntityNotificationsExtensions
    {
        public static SendNotificationCommandMessage CreateProjectEditOwnerNotification(this ProjectEntity project, ProjectUserEntity user)
        {
            var quotes = new NotificationQuotedEntity[]
            {
                new(user.Id, user.UserAvatarUrl!, user.UserName),
                new(project.Id, project.AvatarUrl, project.ProjectName)
            };

            return new SendNotificationCommandMessage(
                Title: "Смена владельца проекта",
                Body: $"Владелец проекта {project.ProjectName} изменен на пользователя {user.UserName}",
                project.OwnerUserId,
                Type: "ProjectEditOwner",
                project.AuditInfo.UpdatedAt,
                project.Id,
                quotes
            );
        }
    }
}