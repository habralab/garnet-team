using Garnet.Teams.Application;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public record TeamUserDocument
    {
        public string Id { get; init; } = null!;
        public string UserId { get; init; } = null!;
        public string Username { get; init; } = null!;

        public static TeamUserDocument Create(string id, string userId, string username)
        {
            return new TeamUserDocument
            {
                Id = id,
                UserId = userId,
                Username = username
            };
        }

        public static TeamUser ToDomain(TeamUserDocument doc)
        {
            return new TeamUser(doc.Id);
        }
    }
}