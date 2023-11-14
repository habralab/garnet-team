using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Teams.Application.Team;

namespace Garnet.Teams.Infrastructure.MongoDb.Team
{
    public record TeamDocument : DocumentBase
    {
        public string Id { get; init; } = null!;
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string OwnerUserId { get; init; } = null!;
        public string[] Tags { get; init; } = null!;
        public string AvatarUrl { get; init; } = string.Empty;
        public float TotalScore { get; init; } = 0;

        public static TeamDocument Create(string id, string name, string description, string ownerUserId,
            string avatarUrl, string[] tags, float totalScore)
        {
            return new TeamDocument
            {
                Id = id,
                Name = name,
                Description = description,
                OwnerUserId = ownerUserId,
                AvatarUrl = avatarUrl,
                Tags = tags,
                TotalScore = totalScore
            };
        }

        public static TeamEntity ToDomain(TeamDocument doc)
        {
            var audit = AuditInfoDocument.ToDomain(doc.AuditInfo);
            return new TeamEntity(doc.Id, doc.Name, doc.Description, doc.OwnerUserId, doc.AvatarUrl, doc.Tags,
                doc.TotalScore, audit);
        }
    }
}