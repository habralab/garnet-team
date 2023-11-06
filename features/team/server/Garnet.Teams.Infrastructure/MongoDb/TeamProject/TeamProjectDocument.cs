using Garnet.Teams.Application.TeamProject;

namespace Garnet.Teams.Infrastructure.MongoDb.TeamProject
{
    public record TeamProjectDocument
    {
        public string Id { get; init; } = null!;
        public string TeamId { get; init; } = null!;
        public string ProjectId { get; init; } = null!;

        public static TeamProjectDocument Create(string id, string teamId, string projectId)
        {
            return new TeamProjectDocument
            {
                Id = id,
                ProjectId = projectId,
                TeamId = teamId
            };
        }

        public static TeamProjectEntity ToDomain(TeamProjectDocument doc)
        {
            return new TeamProjectEntity(doc.Id, doc.ProjectId, doc.TeamId);
        }
    }
}