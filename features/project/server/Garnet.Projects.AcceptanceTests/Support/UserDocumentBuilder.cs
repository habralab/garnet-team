using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.Support;

namespace Garnet.Projects.AcceptanceTests.Support;

public class UserDocumentBuilder
{
    private string _id = Uuid.NewMongo();

    public UserDocumentBuilder WithId(string id)
    {
        _id = id;
        return this;
    }
}

public static partial class GiveMeExtensions
{
    public static UserDocumentBuilder User(this GiveMe _)
    {
        return new UserDocumentBuilder();
    }
}