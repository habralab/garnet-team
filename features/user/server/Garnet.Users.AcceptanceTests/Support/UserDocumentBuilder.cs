using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.Support;
using Garnet.Users.Infrastructure.MongoDb;

namespace Garnet.Users.AcceptanceTests.Support;

public class UserDocumentBuilder
{
    private string _id = Uuid.NewMongo();
    private readonly AuditInfoDocument _auditInfo = AuditInfoDocument.Create(DateTimeOffset.UtcNow, "system");
    private string _userName = "Username";
    private string _description = "Description";
    private string _avatarUrl = "";
    private List<string> _tags = new();
    private float _totalScore = 0;
    private Dictionary<string, float> _skillScore = new();

    public UserDocumentBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    public UserDocumentBuilder WithUserName(string userName)
    {
        _userName = userName;
        return this;
    }

    public UserDocumentBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public UserDocumentBuilder WithTags(params string[] tags)
    {
        _tags.AddRange(tags);
        return this;
    }

    public UserDocumentBuilder WithAvatarUrl(string avatarUrl)
    {
        _avatarUrl = avatarUrl;
        return this;
    }

    public UserDocumentBuilder WithTotalScore(float totalScore)
    {
        _totalScore = totalScore;
        return this;
    }

    public UserDocumentBuilder WithSkillScore(Dictionary<string, float> skillScore)
    {
        _skillScore = skillScore;
        return this;
    }

    public UserDocument Build()
    {
        var document = UserDocument.Create(
            new UserDocumentCreateArgs(_id, _userName, _description, _avatarUrl, _tags.ToArray(), _totalScore,
                _skillScore)
        );
        return document with { AuditInfo = _auditInfo };
    }

    public static implicit operator UserDocument(UserDocumentBuilder builder)
    {
        return builder.Build();
    }
}

public static class GiveMeExtensions
{
    public static UserDocumentBuilder User(this GiveMe _)
    {
        return new UserDocumentBuilder();
    }
}