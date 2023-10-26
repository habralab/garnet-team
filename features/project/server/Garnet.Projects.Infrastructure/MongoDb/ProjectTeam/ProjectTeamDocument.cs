using Garnet.Projects.Application.ProjectTeam;

namespace Garnet.Projects.Infrastructure.MongoDb.ProjectTeam
{
    public record ProjectTeamDocument
    {
        public string Id { get; init; } = null!;
        public string TeamName { get; init; } = null!;
        public string OwnerUserId { get; set; } = null!;
        public string? TeamAvatarUrl { get; set; } = null!;
        public string[] UserParticipantIds { get; set; } = null!;

        public static ProjectTeamDocument Create(string id, string teamName, string ownerUserId, string? teamAvatarUrl, string[] userParticipants)
        {
            return new ProjectTeamDocument
            {
                Id = id,
                TeamName = teamName,
                OwnerUserId = ownerUserId,
                TeamAvatarUrl = teamAvatarUrl,
                UserParticipantIds = userParticipants
            };
        }

        public static ProjectTeamEntity ToDomain(ProjectTeamDocument doc)
        {
            return new ProjectTeamEntity(doc.Id, doc.TeamName, doc.OwnerUserId, doc.TeamAvatarUrl, doc.UserParticipantIds);
        }
    }
}