using Garnet.Common.Infrastructure.MongoDb;
using Garnet.NewsFeed.Application.NewsFeedTeam;
using Garnet.NewsFeed.Application.NewsFeedTeamParticipant;

namespace Garnet.NewsFeed.Infrastructure.MongoDB.NewsFeedTeamParticipant
{
    public record NewsFeedTeamParticipantDocument
    {
        public string Id { get; init; } = null!;
        public string TeamId { get; init; } = null!;
        public string UserId { get; init; } = null!;

        public static NewsFeedTeamParticipantDocument Create(string id, string teamId, string userId)
        {
            return new NewsFeedTeamParticipantDocument()
            {
                Id = id,
                TeamId = teamId,
                UserId = userId
            };
        }

        public static NewsFeedTeamParticipantEntity ToDomain(NewsFeedTeamParticipantDocument doc)
        {
            return new NewsFeedTeamParticipantEntity(doc.Id, doc.TeamId, doc.UserId);
        }
    }
}