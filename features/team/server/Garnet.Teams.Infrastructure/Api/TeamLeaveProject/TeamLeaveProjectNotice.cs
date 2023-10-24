namespace Garnet.Teams.Infrastructure.Api.TeamLeaveProject
{
    public record TeamLeaveProjectNotice(
        string TeamId,
        string ProjectId
    );
}