using Garnet.Teams.Application;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public record TeamParticipantDocument
    {
        public string Id { get; init; } = null!;
        public string UserId { get; init; } = null!;
        public string TeamId { get; init; } = null!;

        public static TeamParticipantDocument Create(string id, string userId, string teamId)
        {
            return new TeamParticipantDocument
            {
                Id = id,
                UserId = userId,
                TeamId = teamId
            };
        }

        public static TeamParticipant ToDomain(TeamParticipantDocument doc)
        {
            return new TeamParticipant(doc.Id, doc.UserId, doc.TeamId);
        }
    }
}