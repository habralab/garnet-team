namespace Garnet.Teams.Events.TeamJoinInvitation
{
    public record TeamJoinInvitationCancelledEvent(
        string Id,
        string UserId,
        string TeamId
    );
}