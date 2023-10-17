namespace Garnet.Teams.Infrastructure.Api.TeamJoinInvite
{
    public record TeamJoinInvitePayload(
        string Id,
        string UserId,
        string TeamId,
        DateTime Created,
        string CreatedBy
    );
}