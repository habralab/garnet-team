using Garnet.Projects.Application.ProjectTeam;

namespace Garnet.Projects.Infrastructure.MongoDb.ProjectTeam
{
    public record ProjectTeamDocument
    {
        public string Id { get; init; } = null!;
        public string TeamName { get; init; } = null!;
        public string OwnerUserId { get; set; } = null!;

        public static ProjectTeamDocument Create(string id, string teamName, string ownerUserId)
        {
            return new ProjectTeamDocument
            {
                Id = id,
                TeamName = teamName,
                OwnerUserId = ownerUserId
            };
        }

        public static ProjectTeamEntity ToDomain(ProjectTeamDocument doc)
        {
            return new ProjectTeamEntity(doc.Id, doc.TeamName, doc.OwnerUserId);
        }
    }
}