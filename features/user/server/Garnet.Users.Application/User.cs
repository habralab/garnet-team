using Garnet.Users.Events;

namespace Garnet.Users.Application;

public record User(
    string Id,
    string UserName,
    string Description,
    string AvatarUrl,
    string[] Tags,
    float TotalScore,
    Dictionary<string, float> SkillScore
);

public static class UserDocumentExtensions
{
    public static UserUpdatedEvent ToUpdatedEvent(this User doc)
    {
        return new UserUpdatedEvent(doc.Id, doc.UserName, doc.Description, doc.AvatarUrl, doc.Tags, doc.TotalScore, doc.SkillScore);
    }

    public static UserCreatedEvent ToCreatedEvent(this User doc)
    {
        return new UserCreatedEvent(doc.Id, doc.UserName);
    }
}