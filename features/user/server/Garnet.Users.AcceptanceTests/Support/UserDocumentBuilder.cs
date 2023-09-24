using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.Support;
using Garnet.Users.Infrastructure.MongoDb;

namespace Garnet.Users.AcceptanceTests.Support;

public class UserDocumentBuilder
{
    private string _id = Uuid.NewMongo();
    private string _identityId = Uuid.NewGuid();
    private string _userName = "Username";
    private string _description = "Description";
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
    
    public UserDocument Build()
    {
        return UserDocument.Create(_id, _identityId, _userName, _description, _tags.ToArray());
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