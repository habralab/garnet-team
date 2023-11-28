namespace Garnet.Teams.Infrastructure.Api.TeamJoinProjectRequest
{
    public record TeamJoinProjectRequestInput(
        string TeamId,
        string ProjectId
    );
}