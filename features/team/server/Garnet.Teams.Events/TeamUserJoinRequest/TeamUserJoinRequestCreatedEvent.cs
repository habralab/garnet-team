namespace Garnet.Teams.Events.TeamUserJoinRequest
{
    public record TeamUserJoinRequestCreatedEvent(
        string Id,
        string UserId,
        string TeamId
    );
}