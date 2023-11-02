using Garnet.Projects.Application.ProjectTeamJoinRequest;

namespace Garnet.Projects.Infrastructure.MongoDb.ProjectTeamJoinRequest
{
    public record ProjectTeamJoinRequestDocument
    {
        public string Id { get; init; } = null!;
        public string TeamId { get; init; } = null!;
        public string TeamName { get; init; } = null!;
        public string ProjectId { get; init; } = null!;

        public static ProjectTeamJoinRequestDocument Create(string id, string teamId, string teamName, string projectId)
        {
            return new ProjectTeamJoinRequestDocument
            {
                Id = id,
                TeamId = teamId,
                TeamName = teamName,
                ProjectId = projectId
            };
        }

        public static ProjectTeamJoinRequestEntity ToDomain(ProjectTeamJoinRequestDocument doc)
        {
            return new ProjectTeamJoinRequestEntity(doc.Id, doc.TeamId, doc.TeamName, doc.ProjectId);
        }
    }
}