using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Infrastructure.MongoDb.ProjectUser;

namespace Garnet.Projects.AcceptanceTests.Support;

public class ProjectUserDocumentBuilder
{
    private string _id = Uuid.NewMongo();
    private string _userName = "Username";
    private string? _userAvatarUrl = null!;


    public ProjectUserDocumentBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    public ProjectUserDocumentBuilder WithUserName(string userName)
    {
        _userName = userName;
        return this;
    }


    public ProjectUserDocument Build()
    {
        return ProjectUserDocument.Create(_id, _userName, _userAvatarUrl);
    }

    public static implicit operator ProjectUserDocument(ProjectUserDocumentBuilder builder)
    {
        return builder.Build();
    }
}

public static partial class GiveMeExtensions
{
    public static ProjectUserDocumentBuilder ProjectUser(this GiveMe _)
    {
        return new ProjectUserDocumentBuilder();
    }
}
