using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Projects.Application.ProjectTask;

namespace Garnet.Projects.Infrastructure.MongoDb.ProjectTask;

public record ProjectTaskDocument : DocumentBase
{
    public string Id { get; init; } = null!;
    public int TaskNumber { get; init; } = 0;
    public string ProjectId { get; init; } = null!;
    public string ResponsibleUserId { get; init; } = null!;
    public string Name { get; init; } = null!;
    public string? Description { get; init; } = null!;
    public string Status { get; init; } = null!;
    public string[] TeamExecutorIds { get; init; } = null!;
    public string[] UserExecutorIds { get; init; } = null!;
    public string[] Tags { get; init; } = null!;
    public string[] Labels { get; init; } = null!;
    public bool Reopened { get; init; } = false;

    public static ProjectTaskDocument Create(
        string id,
        int taskNumber,
        string projectId,
        string responsibleUserId,
        string name,
        string? description,
        string status,
        string[] teamExecutorIds,
        string[] userExecutorIds,
        string[] tags,
        string[] labels,
        bool reopened
    )
    {
        return new ProjectTaskDocument
        {
            Id = id,
            TaskNumber = taskNumber,
            ProjectId = projectId,
            ResponsibleUserId = responsibleUserId,
            Name = name,
            Description = description,
            Status = status,
            TeamExecutorIds = teamExecutorIds,
            UserExecutorIds = userExecutorIds,
            Tags = tags,
            Labels = labels,
            Reopened = reopened
        };
    }

    public static ProjectTaskEntity ToDomain(ProjectTaskDocument doc)
    {
        var auditInfo = AuditInfoDocument.ToDomain(doc.AuditInfo);
        return new ProjectTaskEntity(
            doc.Id,
            doc.TaskNumber,
            doc.ProjectId,
            doc.ResponsibleUserId,
            doc.Name,
            doc.Description,
            doc.Status,
            doc.TeamExecutorIds,
            doc.UserExecutorIds,
            doc.Tags,
            doc.Labels,
            doc.Reopened,
            auditInfo
        );
    }
}