namespace Garnet.Teams.Events.TeamUserJoinRequest
{
    public record TeamUserJoinRequestDecidedEvent(
        string Id,
        string UserId,
        string TeamId,
        bool IsApproved
    );
}