using Garnet.Teams.Application.ProjectTeamParticipant;

namespace Garnet.Teams.Infrastructure.MongoDb.ProjectTeamParticipant
{
    public record ProjectTeamParticipantDocument
    {
        public string Id { get; init; } = null!;
        public string TeamId { get; init; } = null!;
        public string ProjectId { get; init; } = null!;

        public static ProjectTeamParticipantDocument Create(string id, string teamId, string projectId)
        {
            return new ProjectTeamParticipantDocument
            {
                Id = id,
                ProjectId = projectId,
                TeamId = teamId
            };
        }

        public static ProjectTeamParticipantEntity ToDomain(ProjectTeamParticipantDocument doc)
        {
            return new ProjectTeamParticipantEntity(doc.Id, doc.ProjectId, doc.TeamId);
        }
    }
}