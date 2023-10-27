using Garnet.Common.Application;
using Garnet.Projects.Events.ProjectTask;

namespace Garnet.Projects.Application.ProjectTask;

public record ProjectTaskEntity(
    string Id,
    int TaskNumber,
    string ProjectId,
    string UserCreatorId,
    string Name,
    string? Description,
    string Status,
    string? TeamExecutorId,
    string[] UserExecutorIds,
    string[] Tags,
    string[] Labels,
    AuditInfo AuditInfo
);

public static class ProjectTaskEntityExtensions
{
    public static ProjectTaskCreatedEvent ToCreatedEvent(this ProjectTaskEntity doc)
    {
        return new ProjectTaskCreatedEvent(doc.Id, doc.TaskNumber, doc.ProjectId, doc.UserCreatorId, doc.Name,
            doc.Description,
            doc.Status, doc.TeamExecutorId, doc.UserExecutorIds, doc.Tags, doc.Labels);
    }

    public static ProjectTaskUpdatedEvent ToUpdatedEvent(this ProjectTaskEntity doc)
    {
        return new ProjectTaskUpdatedEvent(doc.Id, doc.TaskNumber, doc.ProjectId, doc.UserCreatorId, doc.Name,
            doc.Description,
            doc.Status, doc.TeamExecutorId, doc.UserExecutorIds, doc.Tags, doc.Labels);
    }

    public static ProjectTaskDeletedEvent ToDeletedEvent(this ProjectTaskEntity doc)
    {
        return new ProjectTaskDeletedEvent(doc.Id, doc.TaskNumber, doc.ProjectId, doc.UserCreatorId, doc.Name,
            doc.Description,
            doc.Status, doc.TeamExecutorId, doc.UserExecutorIds, doc.Tags, doc.Labels);
    }
}