namespace Garnet.Teams.Infrastructure.Api.TeamJoinInvite
{
    public record TeamJoinInvitePayload(
        string Id,
        string UserId,
        string TeamId
    ) : TeamJoinInviteInput(UserId, TeamId);
}