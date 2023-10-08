namespace Garnet.Teams.Events
{
    public record TeamUserJoinRequestCreatedEvent(
        string Id,
        string UserId,
        string TeamId
    );
}