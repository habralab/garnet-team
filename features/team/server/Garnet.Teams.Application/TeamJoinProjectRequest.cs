namespace Garnet.Teams.Application
{
    public record TeamJoinProjectRequest(
        string Id,
        string TeamId,
        string ProjectId
    );
}