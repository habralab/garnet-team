namespace Garnet.Teams.Infrastructure.Api.TeamJoinInvite
{
    public record TeamJoinInviteInput(
        string UserId,
        string TeamId
    );
}