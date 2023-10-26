namespace Garnet.Teams.Infrastructure.Api.TeamLeaveProject
{
    public record TeamLeaveProjectInput(
        string TeamId,
        string ProjectId
    );
}