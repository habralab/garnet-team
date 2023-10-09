namespace Garnet.Teams.Events.TeamJoinProjectRequest
{
    public record TeamJoinProjectRequestCreatedEvent(
        string Id,
        string ProjectId,
        string TeamId
    );
}