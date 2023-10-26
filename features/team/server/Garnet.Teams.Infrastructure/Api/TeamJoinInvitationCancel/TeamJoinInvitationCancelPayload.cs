using Garnet.Teams.Infrastructure.Api.TeamJoinInvite;

namespace Garnet.Teams.Infrastructure.Api.TeamJoinInvitationCancel
{
    public record TeamJoinInvitationCancelPayload(
        string Id,
        string UserId,
        string TeamId,
        DateTimeOffset CreatedAt
    ) : TeamJoinInvitePayload(Id, UserId, TeamId, CreatedAt);
}