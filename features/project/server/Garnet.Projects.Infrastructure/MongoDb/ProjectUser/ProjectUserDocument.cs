using Garnet.Projects.Application;

namespace Garnet.Projects.Infrastructure.MongoDb;

public record ProjectUserDocument
{
    public string Id { get; init; } = null!;

    public static ProjectUserDocument Create(string id)
    {
        return new ProjectUserDocument
        {
            Id = id
        };
    }

    public static ProjectUser ToDomain(ProjectUserDocument doc)
    {
        return new ProjectUser(doc.Id);
    }
}