namespace Garnet.Teams.Events.TeamJoinProjectRequest
{
    public record TeamLeaveProjectEvent(
        string TeamId,
        string ProjectId
    );
}