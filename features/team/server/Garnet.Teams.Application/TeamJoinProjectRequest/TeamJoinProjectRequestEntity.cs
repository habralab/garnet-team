using Garnet.Teams.Events.TeamJoinProjectRequest;

namespace Garnet.Teams.Application.TeamJoinProjectRequest
{
    public record TeamJoinProjectRequestEntity(
        string Id,
        string TeamId,
        string ProjectId
    );

    public static class TeamJoinProjectRequestEntityExtensions
    {
        public static TeamJoinProjectRequestCreatedEvent ToCreatedEvent(this TeamJoinProjectRequestEntity entity)
        {
            return new TeamJoinProjectRequestCreatedEvent(entity.Id, entity.ProjectId, entity.TeamId);
        }
    }
}