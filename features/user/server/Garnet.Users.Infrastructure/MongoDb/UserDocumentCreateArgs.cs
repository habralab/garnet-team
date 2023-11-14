namespace Garnet.Users.Infrastructure.MongoDb;

public record UserDocumentCreateArgs(
    string Id,
    string UserName, 
    string Description, 
    string AvatarUrl, 
    string[] Tags,
    float TotalScore,
    Dictionary<string, float> SkillScore);