using Garnet.Teams.Application;
using Garnet.Teams.Application.TeamJoinInvitation.Entities;

namespace Garnet.Teams.Infrastructure.MongoDb.TeamJoinInvitation
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

        public static TeamJoinInvitationEntity ToDomain(TeamJoinInvitationDocument doc)
        {
            return new TeamJoinInvitationEntity(doc.Id, doc.UserId, doc.TeamId);
        }
    }
}