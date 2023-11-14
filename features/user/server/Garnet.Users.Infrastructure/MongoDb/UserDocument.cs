using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Users.Application;

namespace Garnet.Users.Infrastructure.MongoDb;

public record UserDocument : DocumentBase
{
    public string Id { get; init; } = null!;
    public string UserName { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string AvatarUrl { get; init; } = null!;
    public string[] Tags { get; init; } = Array.Empty<string>();
    public float TotalScore { get; init; } = 0;
    public Dictionary<string, float> SkillScore { get; init; } = new();

    public static UserDocument Create(UserDocumentCreateArgs args)
    {
        return new UserDocument
        {
            Id = args.Id,
            UserName = args.UserName,
            Description = args.Description,
            AvatarUrl = args.AvatarUrl,
            Tags = args.Tags,
            TotalScore = args.TotalScore,
            SkillScore = args.SkillScore
        };
    }
    
    public static User ToDomain(UserDocument doc)
    {
        return new User(doc.Id, doc.UserName, doc.Description, doc.AvatarUrl, doc.Tags, doc.TotalScore, doc.SkillScore);
    }
}