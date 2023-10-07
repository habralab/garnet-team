namespace Garnet.Teams.Infrastructure.Api.TeamJoinInvite
{
    public record TeamJoinInvitePayload(
        string UserId,
        string TeamId
    );
}