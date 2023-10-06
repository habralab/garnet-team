namespace Garnet.Teams.Events
{
    public record TeamUserJoinRequestProcessedEvent(
        string UserId,
        string TeamId,
        bool IsApproved
    );
}