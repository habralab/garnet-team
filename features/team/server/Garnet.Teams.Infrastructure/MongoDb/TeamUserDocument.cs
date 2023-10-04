using Garnet.Teams.Application;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public record TeamUserDocument
    {
        public string Id { get; init; } = null!;
        public string Username { get; init; } = null!;

        public static TeamUserDocument Create(string userId, string username)
        {
            return new TeamUserDocument
            {
                Id = userId,
                Username = username
            };
        }

        public static TeamUser ToDomain(TeamUserDocument doc)
        {
            return new TeamUser(doc.Id);
        }
    }
}