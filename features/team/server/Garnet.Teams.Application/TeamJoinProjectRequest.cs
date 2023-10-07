namespace Garnet.Teams.Application
{
    public record TeamJoinProjectRequest(
        string TeamId,
        string ProjectId
    );
}