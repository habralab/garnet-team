using Garnet.Projects.Application.Project;

namespace Garnet.Projects.Infrastructure.MongoDb.Project;

public record ProjectDocument
{
    public string Id { get; init; } = null!;
    public string OwnerUserId { get; init; } = null!;
    public string ProjectName { get; init; } = null!;
    public string? Description { get; init; } = null!;
    public string? AvatarUrl { get; init; } = null!;
    public string[] Tags { get; init; } = null!;

    public static ProjectDocument Create(string id, string ownerUserId, string projectName, string? description,
        string? avatarUrl, string[] tags)
    {
        return new ProjectDocument
        {
            Id = id,
            OwnerUserId = ownerUserId,
            ProjectName = projectName,
            Description = description,
            AvatarUrl = avatarUrl,
            Tags = tags
        };
    }

    public static ProjectEntity ToDomain(ProjectDocument doc)
    {
        return new ProjectEntity(doc.Id, doc.OwnerUserId, doc.ProjectName, doc.Description, doc.AvatarUrl, doc.Tags);
    }
}