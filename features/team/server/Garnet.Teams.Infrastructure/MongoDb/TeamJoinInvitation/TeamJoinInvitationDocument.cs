using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Teams.Application.TeamJoinInvitation;

namespace Garnet.Teams.Infrastructure.MongoDb.TeamJoinInvitation
{
    public record TeamJoinInvitationDocument : DocumentBase
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
            var auditInfo = new TeamJoinInvitationAuditInfo(doc.AuditInfo.CreatedAt, doc.AuditInfo.CreatedBy);
            return new TeamJoinInvitationEntity(doc.Id, doc.UserId, doc.TeamId, auditInfo);
        }
    }
}