using Garnet.Teams.Application;

namespace Garnet.Teams.Infrastructure.MongoDb
{
    public class TeamJoinProjectRequestDocument
    {
        public string Id { get; init; } = null!;
        public string TeamId { get; init; } = null!;
        public string ProjectId { get; init; } = null!;

        public static TeamJoinProjectRequestDocument Create(string id, string teamId, string projectId)
        {
            return new TeamJoinProjectRequestDocument()
            {
                Id = id,
                TeamId = teamId,
                ProjectId = projectId
            };
        }

        public static TeamJoinProjectRequest ToDomain(TeamJoinProjectRequestDocument doc)
        {
            return new TeamJoinProjectRequest(doc.Id, doc.TeamId, doc.ProjectId);
        }
    }
}