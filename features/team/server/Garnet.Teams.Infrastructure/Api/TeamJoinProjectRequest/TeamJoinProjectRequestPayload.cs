namespace Garnet.Teams.Infrastructure.Api.TeamJoinProjectRequest
{
    public record TeamJoinProjectRequestPayload(
        string Id,
        string TeamId,
        string ProjectId
    );
}