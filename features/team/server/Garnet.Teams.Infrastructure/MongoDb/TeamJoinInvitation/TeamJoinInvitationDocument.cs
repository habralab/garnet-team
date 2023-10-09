using Garnet.Teams.Application;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public class TeamJoinInvitationDocument
    {
        public string Id { get; init; } = null!;
        public string UserId { get; init; } = null!;
        public string TeamId { get; init; } = null!;

        public static TeamJoinInvitationDocument Create(string id, string userId, string teamId)
        {
            return new TeamJoinInvitationDocument
            {
                Id = id,
                UserId = userId,
                TeamId = teamId
            };
        }

        public static TeamJoinInvitation ToDomain(TeamJoinInvitationDocument doc)
        {
            return new TeamJoinInvitation(doc.Id, doc.UserId, doc.TeamId);
        }
    }
}