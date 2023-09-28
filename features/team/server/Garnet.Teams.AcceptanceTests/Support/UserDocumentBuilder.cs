using Garnet.Common.Infrastructure.Support;

namespace Garnet.Teams.AcceptanceTests.Support
{
    public class UserDocumentBuilder
    {
        public string Id { get; private set; } = Uuid.NewMongo();

        public UserDocumentBuilder WithId(string id)
        {
            Id = id;
            return this;
        }
    }
}