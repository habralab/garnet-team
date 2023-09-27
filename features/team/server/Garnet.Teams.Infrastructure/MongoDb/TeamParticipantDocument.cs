using Garnet.Teams.Application;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public record TeamParticipantDocument
    {
        public string UserId { get; init; } = null!;
        public string TeamId { get; init; } = null!;

        public static TeamParticipantDocument Create(string userId, string teamId)
        {
            return new TeamParticipantDocument
            {
                UserId = userId,
                TeamId = teamId
            };
        }

        public static TeamParticipant ToDomain(TeamParticipantDocument doc)
        {
            return new TeamParticipant(doc.UserId, doc.TeamId);
        }
    }
}