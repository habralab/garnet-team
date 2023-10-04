using Garnet.Projects.Application;

namespace Garnet.Projects.Infrastructure.MongoDb
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
                TeamId = teamId,
                ProjectId = projectId
            };
        }

        public static ProjectTeamParticipant ToDomain(ProjectTeamParticipantDocument doc)
        {
            return new ProjectTeamParticipant(doc.Id, doc.TeamId, doc.ProjectId);
        }
    }
}