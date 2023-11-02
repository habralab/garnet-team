using Garnet.Teams.Application.TeamUser;

namespace Garnet.Teams.Infrastructure.MongoDb.TeamUser
{
    public record TeamUserDocument
    {
        public string Id { get; init; } = null!;
        public string Username { get; init; } = null!;
        public string? AvatarUrl { get; init; }

        public static TeamUserDocument Create(string userId, string username, string? avatarUrl)
        {
            return new TeamUserDocument
            {
                Id = userId,
                Username = username,
                AvatarUrl = avatarUrl
            };
        }

        public static TeamUserEntity ToDomain(TeamUserDocument doc)
        {
            return new TeamUserEntity(doc.Id, doc.Username);
        }
    }
}