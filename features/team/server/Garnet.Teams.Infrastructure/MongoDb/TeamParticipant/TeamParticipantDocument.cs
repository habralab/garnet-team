using Garnet.Teams.Application.TeamParticipant;

namespace Garnet.Teams.Infrastructure.MongoDb.TeamParticipant
{
    public record TeamParticipantDocument
    {
        public string Id { get; init; } = null!;
        public string UserId { get; init; } = null!;
        public string Username { get; init; } = null!;
        public string AvatarUrl { get; init; } = string.Empty;
        public string TeamId { get; init; } = null!;

        public static TeamParticipantDocument Create(string id, string userId, string username, string teamId, string avatarUrl)
        {
            return new TeamParticipantDocument
            {
                Id = id,
                UserId = userId,
                Username = username,
                TeamId = teamId,
                AvatarUrl = avatarUrl
            };
        }

        public static TeamParticipantEntity ToDomain(TeamParticipantDocument doc)
        {
            return new TeamParticipantEntity(doc.Id, doc.UserId, doc.Username, doc.TeamId, doc.AvatarUrl);
        }
    }
}