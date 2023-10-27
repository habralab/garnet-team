using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Projects.Application.ProjectTask;

namespace Garnet.Projects.Infrastructure.MongoDb.ProjectTask;

public record ProjectTaskDocument : DocumentBase
{
    public string Id { get; init; } = null!;
    public string ProjectId { get; init; } = null!;
    public string UserCreatorId { get; init; } = null!;
    public string Name { get; init; } = null!;
    public string? Description { get; init; } = null!;
    public string Status { get; init; } = null!;
    public string? TeamExecutorId { get; init; } = null!;
    public string? UserExecutorId { get; init; } = null!;
    public string[] Tags { get; init; } = null!;
    public string[] Labels { get; init; } = null!;

    public static ProjectTaskDocument Create(
        string id,
        string projectId,
        string userCreatorId,
        string name,
        string? description,
        string status,
        string? teamExecutorId,
        string? userExecutorId,
        string[] tags,
        string[] labels
    )
    {
        return new ProjectTaskDocument
        {
            Id = id,
            ProjectId = projectId,
            UserCreatorId = userCreatorId,
            Name = name,
            Description = description,
            Status = status,
            TeamExecutorId = teamExecutorId,
            UserExecutorId = userExecutorId,
            Tags = tags,
            Labels = labels
        };
    }

    public static ProjectTaskEntity ToDomain(ProjectTaskDocument doc)
    {
        var auditInfo = AuditInfoDocument.ToDomain(doc.AuditInfo);
        return new ProjectTaskEntity(
            doc.Id,
            doc.ProjectId,
            doc.UserCreatorId,
            doc.Name,
            doc.Description,
            doc.Status,
            doc.TeamExecutorId,
            doc.UserExecutorId,
            doc.Tags,
            doc.Labels,
            auditInfo
        );
    }
}