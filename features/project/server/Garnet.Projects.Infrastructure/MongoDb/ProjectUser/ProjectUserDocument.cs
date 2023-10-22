using Garnet.Projects.Application.ProjectUser;

namespace Garnet.Projects.Infrastructure.MongoDb.ProjectUser;

public record ProjectUserDocument
{
    public string Id { get; init; } = null!;
    public string UserName { get; set; } = null!;
    public string UserAvatarUrl { get; set; } = "";

    public static ProjectUserDocument Create(string id, string userName)
    {
        return new ProjectUserDocument
        {
            Id = id,
            UserName = userName
        };
    }

    public static ProjectUserEntity ToDomain(ProjectUserDocument doc)
    {
        return new ProjectUserEntity(doc.Id, doc.UserName, doc.UserAvatarUrl);
    }
}