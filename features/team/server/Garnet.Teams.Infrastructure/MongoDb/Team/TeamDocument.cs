using Garnet.Teams.Application;
using Garnet.Teams.Application.Team.Entities;

namespace Garnet.Teams.Infrastructure.MongoDb.Team
{
    public record TeamDocument
    {
        public string Id { get; init; } = null!;
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string OwnerUserId { get; init; } = null!;
        public string[] Tags {get;init;} = null!;

        public static TeamDocument Create(string id, string name, string description, string ownerUserId, string[] tags)
        {
            return new TeamDocument
            {
                Id = id,
                Name = name,
                Description = description,
                OwnerUserId = ownerUserId,
                Tags = tags
            };
        }

        public static TeamEntity ToDomain(TeamDocument doc)
        {
            return new TeamEntity(doc.Id, doc.Name, doc.Description, doc.OwnerUserId, doc.Tags);
        }
    }
}