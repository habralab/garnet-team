using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Projects.Application.ProjectTask;

namespace Garnet.Projects.Infrastructure.MongoDb.ProjectTask;

public record ProjectTaskDocument : DocumentBase
{
    public string Id { get; init; } = null!;
    public string ProjectId { get; init; } = null!;
    public string UserCreatorId { get; init; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string? TeamExecutorId { get; set; } = null!;
    public string? UserExecutorId { get; set; } = null!;
    public string[] Tags { get; set; } = null!;

    public static ProjectTaskDocument Create(
        string id,
        string projectId,
        string userCreatorId,
        string name,
        string? description,
        string status,
        string? teamExecutorId,
        string? userExecutorId,
        string[] tags
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
            Tags = tags
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
            auditInfo
        );
    }
}