namespace Garnet.Teams.Application
{
    public record TeamJoinProjectRequestEntity(
        string Id,
        string TeamId,
        string ProjectId
    );
}