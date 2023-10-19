namespace Garnet.Teams.Events.TeamJoinInvitation
{
    public record TeamJoinInvitationDecidedEvent(
        string Id,
        string UserId,
        string TeamId,
        bool IsApproved
    );
}