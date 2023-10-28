using Garnet.Projects.Application.ProjectUser;

namespace Garnet.Projects.Infrastructure.MongoDb.ProjectUser;

public record ProjectUserDocument
{
    public string Id { get; init; } = null!;
    public string UserName { get; init; } = null!;
    public string? UserAvatarUrl { get; init; } = null!;

    public static ProjectUserDocument Create(string id, string userName, string? userAvatarUrl)
    {
        return new ProjectUserDocument
        {
            Id = id,
            UserName = userName,
            UserAvatarUrl = userAvatarUrl
        };
    }

    public static ProjectUserEntity ToDomain(ProjectUserDocument doc)
    {
        return new ProjectUserEntity(doc.Id, doc.UserName, doc.UserAvatarUrl);
    }
}