using Garnet.Common.Infrastructure.Support;

namespace Garnet.Teams.AcceptanceTests.Support
{
    public class UserDocumentBuilder
    {
        private string _id = Uuid.NewMongo();

        public UserDocumentBuilder WithId(string id)
        {
            _id = id;
            return this;
        }
    }
}