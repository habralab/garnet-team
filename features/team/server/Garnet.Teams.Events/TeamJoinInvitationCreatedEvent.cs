namespace Garnet.Teams.Events
{
    public record TeamJoinInvitationCreatedEvent(
        string UserId,
        string TeamId
    );
}