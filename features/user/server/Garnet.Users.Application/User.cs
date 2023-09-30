using Garnet.Users.Events;

namespace Garnet.Users.Application;

public record User(
    string Id,
    string UserName,
    string Description,
    string AvatarUrl,
    string[] Tags
);

public static class UserDocumentExtensions
{
    public static UserUpdatedEvent ToUpdatedEvent(this User doc)
    {
        return new UserUpdatedEvent(doc.Id, doc.UserName, doc.Description, doc.AvatarUrl, doc.Tags);
    }

    public static UserUpdatedEvent ToCreatedEvent(this User doc)
    {
        return new UserUpdatedEvent(doc.Id, doc.UserName, doc.Description, doc.AvatarUrl, doc.Tags);
    }
}