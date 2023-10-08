namespace Garnet.Teams.Events
{
    public record TeamJoinProjectRequestCreatedEvent(
        string Id,
        string ProjectId,
        string TeamId
    );
}