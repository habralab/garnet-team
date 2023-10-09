using Garnet.Teams.Application;
using Garnet.Teams.Application.TeamJoinProjectRequest.Entities;

namespace Garnet.Teams.Infrastructure.MongoDb.TeamJoinProjectRequest
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

        public static TeamJoinProjectRequestEntity ToDomain(TeamJoinProjectRequestDocument doc)
        {
            return new TeamJoinProjectRequestEntity(doc.Id, doc.TeamId, doc.ProjectId);
        }
    }
}