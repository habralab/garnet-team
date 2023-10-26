using Garnet.Common.Application;
using Garnet.Teams.Events.TeamJoinInvitation;

namespace Garnet.Teams.Application.TeamJoinInvitation
{
    public record TeamJoinInvitationEntity(
        string Id,
        string UserId,
        string TeamId,
        AuditInfo AuditInfo
    );

    public static class TeamJoinInvitationEntityExtensions
    {
        public static TeamJoinInvitationCreatedEvent ToCreatedEvent(this TeamJoinInvitationEntity entity)
        {
            return new TeamJoinInvitationCreatedEvent(entity.Id, entity.UserId, entity.TeamId);
        }

        public static TeamJoinInvitationDecidedEvent ToDecidedEvent(this TeamJoinInvitationEntity entity, bool IsApproved)
        {
            return new TeamJoinInvitationDecidedEvent(entity.Id, entity.UserId, entity.TeamId, IsApproved);
        }

        public static TeamJoinInvitationCancelledEvent ToCancelledEvent(this TeamJoinInvitationEntity entity)
        {
            return new TeamJoinInvitationCancelledEvent(entity.Id, entity.UserId, entity.TeamId);
        }
    }
}