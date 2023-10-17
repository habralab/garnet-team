using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.Support;
using Garnet.Users.Infrastructure.MongoDb;

namespace Garnet.Users.AcceptanceTests.Support;

public class UserDocumentBuilder
{
    private string _id = Uuid.NewMongo();
    private AuditInfo _auditInfo = AuditInfo.Create(DateTimeOffset.UtcNow, "system");
    private string _identityId = Uuid.NewGuid();
    private string _userName = "Username";
    private string _description = "Description";
    private string _avatarUrl = "";
    private List<string> _tags = new();

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
    
    public UserDocument Build()
    {
        var document = UserDocument.Create(
            new UserDocumentCreateArgs(_id, _identityId, _userName, _description, _avatarUrl, _tags.ToArray())
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