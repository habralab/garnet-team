using Garnet.Teams.Application;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public record TeamUserDocument
    {
        public string Id { get; init; } = null!;

        public static TeamUserDocument Create(string id)
        {
            return new TeamUserDocument
            {
                Id = id,
            };
        }

        public static TeamUser ToDomain(TeamUserDocument doc)
        {
            return new TeamUser(doc.Id);
        }
    }
}