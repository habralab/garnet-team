using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.Support;

namespace Garnet.Teams.AcceptanceTests.Features.Support
{
    public class UserDocumentBuilder
    {
        private string _id = Uuid.NewMongo();
        private string _userName = "Username";

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
    }

    public static class GiveMeExtensions
    {
        public static UserDocumentBuilder User(this GiveMe _)
        {
            return new UserDocumentBuilder();
        }
    }
}