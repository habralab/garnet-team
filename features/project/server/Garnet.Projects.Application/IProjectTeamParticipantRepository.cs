namespace Garnet.Projects.Application;

public interface IProjectTeamParticipantRepository
{
    Task<ProjectTeamParticipant> AddProjectTeamParticipant(CancellationToken ct, string teamId, string teamName, string? projectId);
    Task<ProjectTeamParticipant[]> GetProjectTeamParticipantsByProjectId(CancellationToken ct, string projectId);
    Task<ProjectTeamParticipant[]> UpdateProjectTeamParticipant(CancellationToken ct, string teamId, string teamName);
}