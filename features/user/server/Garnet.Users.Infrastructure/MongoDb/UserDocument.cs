using Garnet.Users.Application;

namespace Garnet.Users.Infrastructure.MongoDb;

public record UserDocument
{
    public string Id { get; init; } = null!;
    public string IdentityId { get; init; } = null!;
    public string UserName { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string[] Tags { get; init; } = Array.Empty<string>();

    public static UserDocument Create(string id, string identityId, string userName, string description, string[] tags)
    {
        return new UserDocument
        {
            Id = id,
            IdentityId = identityId,
            UserName = userName,
            Description = description,
            Tags = tags
        };
    }

    public static User ToDomain(UserDocument doc)
    {
        return new User(doc.Id, doc.UserName, doc.Description, doc.Tags);
    }
}