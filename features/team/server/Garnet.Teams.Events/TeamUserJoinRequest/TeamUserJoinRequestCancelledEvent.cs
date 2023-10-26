namespace Garnet.Teams.Events.TeamUserJoinRequest
{
    public record TeamUserJoinRequestCancelledEvent(
        string Id,
        string UserId,
        string TeamId
    );
}