namespace Garnet.Teams.Application
{
    public record TeamJoinInvitation(
        string Id,
        string UserId,
        string TeamId
    );
}