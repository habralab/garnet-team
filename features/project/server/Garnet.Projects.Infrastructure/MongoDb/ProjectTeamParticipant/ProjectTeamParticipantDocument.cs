using Garnet.Projects.Application.ProjectTeamParticipant;
using Garnet.Projects.Infrastructure.MongoDb.Project;
using Garnet.Projects.Infrastructure.MongoDb.ProjectUser;

namespace Garnet.Projects.Infrastructure.MongoDb.ProjectTeamParticipant;

public record ProjectTeamParticipantDocument
{
    public string Id { get; init; } = null!;
    public string TeamId { get; init; } = null!;
    public string TeamName { get; init; } = null!;
    public string ProjectId { get; init; } = null!;
    public string? TeamAvatarUrl { get; init; } = null!;
    public ProjectUserDocument[] UserParticipants { get; init; } = null!;
    public ProjectDocument[] Projects { get; init; } = null!;

    public static ProjectTeamParticipantDocument Create(string id, string teamId, string teamName, string projectId,
        string? teamAvatarUrl, ProjectUserDocument[] userParticipants, ProjectDocument[] projects)
    {
        return new ProjectTeamParticipantDocument
        {
            Id = id,
            TeamId = teamId,
            TeamName = teamName,
            ProjectId = projectId,
            TeamAvatarUrl = teamAvatarUrl,
            UserParticipants = userParticipants,
            Projects = projects
        };
    }

    public static ProjectTeamParticipantEntity ToDomain(ProjectTeamParticipantDocument doc)
    {
        return new ProjectTeamParticipantEntity(doc.Id, doc.TeamId, doc.TeamName, doc.ProjectId, doc.TeamAvatarUrl,
            doc.UserParticipants.Select(ProjectUserDocument.ToDomain).ToArray(),
            doc.Projects.Select(ProjectDocument.ToDomain).ToArray());
    }
}