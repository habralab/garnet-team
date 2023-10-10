using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Infrastructure.MongoDb.Team;

namespace Garnet.Teams.AcceptanceTests.Support
{
    public class TeamDocumentBuilder
    {
        private string _id  = Uuid.NewMongo();
        private string _name  = "TeamName";
        private string _description  = "TeamDescription";
        private string _ownerUserId  = Uuid.NewMongo();
        private string[] _tags = Array.Empty<string>();

        public TeamDocumentBuilder WithId(string id)
        {
            _id = id;
            return this;
        }

        public TeamDocumentBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public TeamDocumentBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public TeamDocumentBuilder WithOwnerUserId(string ownerUserId)
        {
            _ownerUserId = ownerUserId;
            return this;
        }

        public TeamDocument Build()
        {
            return TeamDocument.Create(_id, _name, _description, _ownerUserId, _tags);
        }

        public static implicit operator TeamDocument(TeamDocumentBuilder builder)
        {
            return builder.Build();
        }
    }
}