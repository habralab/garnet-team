using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Infrastructure.MongoDb.Team;

namespace Garnet.Teams.AcceptanceTests.Support
{
    public class TeamDocumentBuilder
    {
        private readonly string _id  = Uuid.NewMongo();
        private string _name  = "TeamName";
        private readonly string _description  = "TeamDescription";
        private readonly string _ownerUserId  = Uuid.NewMongo();
        private readonly string[] _tags = Array.Empty<string>();

        public TeamDocumentBuilder WithName(string name)
        {
            _name = name;
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