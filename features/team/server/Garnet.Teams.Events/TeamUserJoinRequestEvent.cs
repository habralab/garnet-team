namespace Garnet.Teams.Events
{
    public record TeamUserJoinRequestEvent(
        string UserId,
        string TeamId
    );
}