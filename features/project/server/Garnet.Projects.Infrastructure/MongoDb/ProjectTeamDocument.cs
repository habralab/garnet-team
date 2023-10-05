using Garnet.Projects.Application;

namespace Garnet.Projects.Infrastructure.MongoDb
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

        public static ProjectTeam ToDomain(ProjectTeamDocument doc)
        {
            return new ProjectTeam(doc.Id, doc.TeamName, doc.OwnerUserId);
        }
    }
}