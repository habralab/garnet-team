using Garnet.Teams.Application;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public class TeamUserJoinRequestDocument
    {
        public string Id { get; init; } = null!;
        public string UserId { get; init; } = null!;
        public string TeamId { get; init; } = null!;

        public static TeamUserJoinRequestDocument Create(string id, string userId, string teamId)
        {
            return new TeamUserJoinRequestDocument
            {
                Id = id,
                UserId = userId,
                TeamId = teamId
            };
        }

        public static TeamUserJoinRequestEntity ToDomain(TeamUserJoinRequestDocument doc)
        {
            return new TeamUserJoinRequestEntity(doc.Id, doc.UserId, doc.TeamId);
        }
    }
}