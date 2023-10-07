namespace Garnet.Teams.Events
{
    public record TeamJoinProjectRequestCreatedEvent(
        string ProjectId,
        string TeamId
    );
}