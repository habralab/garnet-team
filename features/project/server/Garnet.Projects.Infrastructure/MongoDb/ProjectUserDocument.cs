using Garnet.Projects.Application;

namespace Garnet.Projects.Infrastructure.MongoDb;

public record ProjectUserDocument
{
    public string Id { get; init; } = null!;
    public string UserId { get; init; } = null!;

    public static ProjectUserDocument Create(string id, string userid)
    {
        return new ProjectUserDocument
        {
            Id = id,
            UserId = userid
        };
    }

    public static ProjectUser ToDomain(ProjectUserDocument doc)
    {
        return new ProjectUser(doc.UserId);
    }
}