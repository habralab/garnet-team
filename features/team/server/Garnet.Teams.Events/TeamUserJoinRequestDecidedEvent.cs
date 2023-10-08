namespace Garnet.Teams.Events
{
    public record TeamUserJoinRequestDecidedEvent(
        string Id,
        string UserId,
        string TeamId,
        bool IsApproved
    );
}