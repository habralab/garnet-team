using Garnet.Projects.Application;

namespace Garnet.Projects.Infrastructure.MongoDb;

public record ProjectDocument
{
    public string Id { get; init; } = null!;
    public string OwnerUserId { get; init; } = null!;
    public string ProjectName { get; init; } = null!;
    public string? Description { get; init; } = null!;

    public static ProjectDocument Create(string id, string ownerUserId, string projectName, string? description)
    {
        return new ProjectDocument
        {
            Id = id,
            OwnerUserId = ownerUserId,
            ProjectName = projectName,
            Description = description
        };
    }

    public static Project ToDomain(ProjectDocument doc)
    {
        return new Project(doc.Id, doc.OwnerUserId, doc.ProjectName, doc.Description);
    }
}