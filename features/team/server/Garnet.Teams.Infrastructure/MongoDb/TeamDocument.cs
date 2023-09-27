using Garnet.Teams.Application;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public record TeamDocument
    {
        public string Id { get; init; } = null!;
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string OwnerUserId { get; init; } = null!;

        public static TeamDocument Create(string id, string name, string description, string ownerUserId)
        {
            return new TeamDocument
            {
                Id = id,
                Name = name,
                Description = description,
                OwnerUserId = ownerUserId
            };
        }

        public static Team ToDomain(TeamDocument doc)
        {
            return new Team(doc.Id, doc.Name, doc.Description, doc.OwnerUserId);
        }
    }
}