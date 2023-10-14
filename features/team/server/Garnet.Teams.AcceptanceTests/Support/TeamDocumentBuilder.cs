using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.Infrastructure.MongoDb.Team;

namespace Garnet.Teams.AcceptanceTests.Support
{
    public class TeamDocumentBuilder
    {
        private readonly string _id = Uuid.NewMongo();
        private string _name = "TeamName";
        private readonly string _description = "TeamDescription";
        private readonly AuditInfo _auditInfo = AuditInfo.Create(DateTimeOffset.UtcNow, "system");
        private readonly string _ownerUserId = Uuid.NewMongo();
        private readonly string? _avatarUrl = null;
        private readonly string[] _tags = Array.Empty<string>();

        public TeamDocumentBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public TeamDocument Build()
        {
            var document = TeamDocument.Create(_id, _name, _description, _ownerUserId, _avatarUrl, _tags);

            return document with { AuditInfo = _auditInfo };
        }

        public static implicit operator TeamDocument(TeamDocumentBuilder builder)
        {
            return builder.Build();
        }
    }
}