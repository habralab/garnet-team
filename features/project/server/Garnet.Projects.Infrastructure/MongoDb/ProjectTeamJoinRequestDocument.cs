using Garnet.Projects.Application;

namespace Garnet.Projects.Infrastructure.MongoDb
{
    public record ProjectTeamJoinRequestDocument
    {
        public string Id { get; init; } = null!;
        public string TeamId { get; init; } = null!;
        public string TeamName { get; set; } = null!;
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

        public static ProjectTeamJoinRequest ToDomain(ProjectTeamJoinRequestDocument doc)
        {
            return new ProjectTeamJoinRequest(doc.Id, doc.TeamId, doc.TeamName, doc.ProjectId);
        }
    }
}