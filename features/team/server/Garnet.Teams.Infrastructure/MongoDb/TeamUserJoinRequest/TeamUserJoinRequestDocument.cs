using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Teams.Application.TeamUserJoinRequest;

namespace Garnet.Teams.Infrastructure.MongoDb.TeamUserJoinRequest
{
    public record TeamUserJoinRequestDocument : DocumentBase
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
            var auditInfo = new TeamUserJoinRequestAuditInfo(doc.AuditInfo.CreatedAt);
            return new TeamUserJoinRequestEntity(doc.Id, doc.UserId, doc.TeamId, auditInfo);
        }
    }
}