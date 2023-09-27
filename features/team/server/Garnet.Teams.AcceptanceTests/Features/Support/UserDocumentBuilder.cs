using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.Support;

namespace Garnet.Teams.AcceptanceTests.Features.Support
{
    public class UserDocumentBuilder
    {
        private string _id;
        public string Id => _id;

        public UserDocumentBuilder()
        {
            _id = Uuid.NewMongo();
        }

        public UserDocumentBuilder WithId(string id)
        {
            _id = id;
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