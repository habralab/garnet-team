using Garnet.Common.Application;
using Garnet.Projects.Events.ProjectTask;

namespace Garnet.Projects.Application.ProjectTask;

public record ProjectTaskEntity(
    string Id,
    string ProjectId,
    string UserCreatorId,
    string Name,
    string? Description,
    string Status,
    string? TeamExecutorId,
    string? UserExecutorId,
    string[] Tags,
    AuditInfo AuditInfo
);

public static class ProjectTaskEntityExtensions
{
    public static ProjectTaskCreatedEvent ToCreatedEvent(this ProjectTaskEntity doc)
    {
        return new ProjectTaskCreatedEvent(doc.Id, doc.ProjectId, doc.UserCreatorId, doc.Name, doc.Description,
            doc.Status, doc.TeamExecutorId, doc.UserExecutorId, doc.Tags);
    }

    public static ProjectTaskUpdatedEvent ToUpdatedEvent(this ProjectTaskEntity doc)
    {
        return new ProjectTaskUpdatedEvent(doc.Id, doc.ProjectId, doc.UserCreatorId, doc.Name, doc.Description,
            doc.Status, doc.TeamExecutorId, doc.UserExecutorId, doc.Tags);
    }

    public static ProjectTaskDeletedEvent ToDeletedEvent(this ProjectTaskEntity doc)
    {
        return new ProjectTaskDeletedEvent(doc.Id, doc.ProjectId, doc.UserCreatorId, doc.Name, doc.Description,
            doc.Status, doc.TeamExecutorId, doc.UserExecutorId, doc.Tags);
    }
}