using Garnet.Teams.Application;
using Garnet.Teams.Application.TeamParticipant.Entities;

namespace Garnet.Teams.Infrastructure.MongoDb.TeamParticipant
{
    public record TeamParticipantDocument
    {
        public string Id { get; init; } = null!;
        public string UserId { get; init; } = null!;
        public string Username { get; init; } = null!;
        public string TeamId { get; init; } = null!;

        public static TeamParticipantDocument Create(string id, string userId, string username, string teamId)
        {
            return new TeamParticipantDocument
            {
                Id = id,
                UserId = userId,
                Username = username,
                TeamId = teamId
            };
        }

        public static TeamParticipantEntity ToDomain(TeamParticipantDocument doc)
        {
            return new TeamParticipantEntity(doc.Id, doc.UserId, doc.Username, doc.TeamId);
        }
    }
}