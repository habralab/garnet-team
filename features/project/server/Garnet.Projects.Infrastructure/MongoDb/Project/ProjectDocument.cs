using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Projects.Application.Project;

namespace Garnet.Projects.Infrastructure.MongoDb.Project;

public record ProjectDocument : DocumentBase
{
    public string Id { get; init; } = null!;
    public string OwnerUserId { get; init; } = null!;
    public string ProjectName { get; init; } = null!;
    public string? Description { get; init; } = null!;
    public string? AvatarUrl { get; init; } = null!;
    public string[] Tags { get; init; } = null!;
    public int TasksCounter { get; init; } = 0;

    public static ProjectDocument Create(string id, string ownerUserId, string projectName, string? description,
        string? avatarUrl, string[] tags, int tasksCounter)
    {
        return new ProjectDocument
        {
            Id = id,
            OwnerUserId = ownerUserId,
            ProjectName = projectName,
            Description = description,
            AvatarUrl = avatarUrl,
            Tags = tags,
            TasksCounter = tasksCounter
        };
    }

    public static ProjectEntity ToDomain(ProjectDocument doc)
    {
        return new ProjectEntity(doc.Id, doc.OwnerUserId, doc.ProjectName, doc.Description, doc.AvatarUrl, doc.Tags,
            doc.TasksCounter);
    }
}