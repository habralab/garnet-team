using Garnet.Teams.Application.TeamUser;

namespace Garnet.Teams.Infrastructure.MongoDb.TeamUser
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

        public static TeamUserEntity ToDomain(TeamUserDocument doc)
        {
            return new TeamUserEntity(doc.Id, doc.Username);
        }
    }
}