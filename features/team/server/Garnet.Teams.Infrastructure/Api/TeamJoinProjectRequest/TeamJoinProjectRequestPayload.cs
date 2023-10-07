namespace Garnet.Teams.Infrastructure.Api.TeamJoinProjectRequest
{
    public record TeamJoinProjectRequestPayload(
        string TeamId,
        string ProjectId
    );
}