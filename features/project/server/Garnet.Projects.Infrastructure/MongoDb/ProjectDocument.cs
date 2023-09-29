using Garnet.Projects.Application;

namespace Garnet.Projects.Infrastructure.MongoDb;

public record ProjectDocument
{
    public string Id { get; init; } = null!;
    public string OwnerUserId { get; init; } = null!;
    public string ProjectName { get; init; } = null!;

    public static ProjectDocument Create(string id, string ownerUserId, string projectName)
    {
        return new ProjectDocument
        {
            Id = id,
            OwnerUserId = ownerUserId,
            ProjectName = projectName
        };
    }

    public static Project ToDomain(ProjectDocument doc)
    {
        return new Project(doc.Id, doc.OwnerUserId, doc.ProjectName);
    }
}