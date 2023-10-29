namespace Garnet.Projects.Application.ProjectTeamParticipant;

public interface IProjectTeamParticipantRepository
{
    Task<ProjectTeamParticipantEntity> AddProjectTeamParticipant(CancellationToken ct, string teamId, string teamName, string projectId);
    Task<ProjectTeamParticipantEntity[]> GetProjectTeamParticipantsByProjectId(CancellationToken ct, string projectId);
    Task DeleteProjectTeamParticipantsByProjectId(CancellationToken ct, string projectId);
    Task UpdateProjectTeamParticipant(CancellationToken ct, string teamId, string teamName, string? teamAvatarUrl);
    Task AddProjectTeamUserParticipant(CancellationToken ct, string teamId, string userId);
    Task CreateIndexes(CancellationToken ct);
}