using Garnet.Common.Application;
using Garnet.Teams.Events.TeamUserJoinRequest;

namespace Garnet.Teams.Application.TeamUserJoinRequest
{
    public record TeamUserJoinRequestEntity(
        string Id,
        string UserId,
        string TeamId,
        AuditInfo AuditInfo
    );

    public static class TeamUserJoinRequestEntityExtensions
    {
        public static TeamUserJoinRequestCreatedEvent ToCreatedEvent(this TeamUserJoinRequestEntity entity)
        {
            return new TeamUserJoinRequestCreatedEvent(entity.Id, entity.UserId, entity.TeamId);
        }

        public static TeamUserJoinRequestDecidedEvent ToDecidedEvent(this TeamUserJoinRequestEntity entity, bool IsApproved)
        {
            return new TeamUserJoinRequestDecidedEvent(entity.Id, entity.UserId, entity.TeamId, IsApproved);
        }
    }
}