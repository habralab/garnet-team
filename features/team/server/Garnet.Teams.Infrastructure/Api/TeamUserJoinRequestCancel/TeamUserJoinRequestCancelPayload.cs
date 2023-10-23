using Garnet.Teams.Infrastructure.Api.TeamUserJoinRequest;

namespace Garnet.Teams.Infrastructure.Api.TeamUserJoinRequestCancel
{
    public record TeamUserJoinRequestCancelPayload(
        string Id,
        string UserId,
        string TeamId,
        DateTimeOffset CreatedAt
    ) : TeamUserJoinRequestPayload(Id, UserId, TeamId, CreatedAt);
}