using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.ProjectTeamParticipant;
using Garnet.Projects.Application.ProjectUser;

namespace Garnet.Projects.Infrastructure.MongoDb.ProjectTeamParticipant;

public record ProjectTeamParticipantDocument
{
    public string Id { get; init; } = null!;
    public string TeamId { get; init; } = null!;
    public string TeamName { get; set; } = null!;
    public string ProjectId { get; init; } = null!;
    public ProjectUserEntity[] UserParticipants { get; set; } = null!;
    public ProjectEntity[] Projects { get; set; } = null!;

    public static ProjectTeamParticipantDocument Create(string id, string teamId, string teamName, string projectId,
        ProjectUserEntity[] userParticipants, ProjectEntity[] projects)
    {
        return new ProjectTeamParticipantDocument
        {
            Id = id,
            TeamId = teamId,
            TeamName = teamName,
            ProjectId = projectId,
            UserParticipants = userParticipants,
            Projects = projects
        };
    }

    public static ProjectTeamParticipantEntity ToDomain(ProjectTeamParticipantDocument doc)
    {
        return new ProjectTeamParticipantEntity(doc.Id, doc.TeamId, doc.TeamName, doc.ProjectId,
            doc.UserParticipants, doc.Projects);
    }
}