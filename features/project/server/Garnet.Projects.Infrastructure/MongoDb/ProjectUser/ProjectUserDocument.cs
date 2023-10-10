using Garnet.Projects.Application.ProjectUser;

namespace Garnet.Projects.Infrastructure.MongoDb.ProjectUser;

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

    public static ProjectUserEntity ToDomain(ProjectUserDocument doc)
    {
        return new ProjectUserEntity(doc.Id);
    }
}