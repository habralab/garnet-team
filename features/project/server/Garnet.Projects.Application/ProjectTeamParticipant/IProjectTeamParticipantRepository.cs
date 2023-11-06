using Garnet.Projects.Application.Project;

namespace Garnet.Projects.Application.ProjectTeamParticipant;

public interface IProjectTeamParticipantRepository
{
    Task<ProjectTeamParticipantEntity> AddProjectTeamParticipant(CancellationToken ct, string teamId, string teamName, string projectId);
    Task<ProjectTeamParticipantEntity[]> GetProjectTeamParticipantsByProjectId(CancellationToken ct, string projectId);
    Task<ProjectEntity[]> GetProjectsOfUserParticipantByUserId(CancellationToken ct, string userId);
    Task<ProjectEntity[]> GetProjectsOfTeamParticipantByTeamId(CancellationToken ct, string teamId);
    Task DeleteProjectTeamParticipantsByProjectId(CancellationToken ct, string projectId);
    Task<ProjectTeamParticipantEntity?> DeleteProjectTeamParticipantsByTeamIdAndProjectId(CancellationToken ct, string teamId, string projectId);
    Task UpdateProjectTeamParticipant(CancellationToken ct, string teamId, string teamName, string? teamAvatarUrl);
    Task AddProjectTeamUserParticipant(CancellationToken ct, string teamId, string userId);
    Task DeleteProjectTeamUserParticipant(CancellationToken ct, string teamId, string userId);
    Task CreateIndexes(CancellationToken ct);
}