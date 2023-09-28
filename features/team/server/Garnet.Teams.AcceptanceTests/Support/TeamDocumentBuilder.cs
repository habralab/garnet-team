using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Infrastructure.MongoDb;

namespace Garnet.Teams.AcceptanceTests.Support
{
    public class TeamDocumentBuilder
    {
        public string Id { get; private set; } = Uuid.NewMongo();
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public string OwnerUserId { get; private set; } = Uuid.NewMongo();

        public TeamDocumentBuilder WithId(string id)
        {
            Id = id;
            return this;
        }

        public TeamDocumentBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public TeamDocumentBuilder WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public TeamDocument Build()
        {
            return TeamDocument.Create(Id, Name, Description, OwnerUserId);
        }

        public static implicit operator TeamDocument(TeamDocumentBuilder builder)
        {
            return builder.Build();
        }
    }
}