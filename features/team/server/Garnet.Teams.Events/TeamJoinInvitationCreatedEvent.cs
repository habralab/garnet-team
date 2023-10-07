namespace Garnet.Teams.Events
{
    public record TeamJoinInvitationCreatedEvent(
        string Id,
        string UserId,
        string TeamId
    );
}