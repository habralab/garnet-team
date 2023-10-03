namespace Garnet.Teams.Events
{
    public record TeamUserJoinRequestCreatedEvent(
        string UserId,
        string TeamId
    );
}