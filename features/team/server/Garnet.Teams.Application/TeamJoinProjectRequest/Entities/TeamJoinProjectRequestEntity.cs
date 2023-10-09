namespace Garnet.Teams.Application.TeamJoinProjectRequest.Entities
{
    public record TeamJoinProjectRequestEntity(
        string Id,
        string TeamId,
        string ProjectId
    );
}