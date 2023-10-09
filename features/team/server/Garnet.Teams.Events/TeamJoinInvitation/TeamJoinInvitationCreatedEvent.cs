namespace Garnet.Teams.Events.TeamJoinInvitation
{
    public record TeamJoinInvitationCreatedEvent(
        string Id,
        string UserId,
        string TeamId
    );
}