namespace Garnet.Teams.Infrastructure.Api.TeamLeaveProject
{
    public record TeamLeaveProjectPayload(
        string TeamId,
        string ProjectId
    ) : TeamLeaveProjectInput(TeamId, ProjectId);
}