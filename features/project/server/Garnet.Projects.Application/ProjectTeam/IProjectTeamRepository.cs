namespace Garnet.Projects.Application.ProjectTeam;

public interface IProjectTeamRepository
{
    Task<ProjectTeamEntity> AddProjectTeam(CancellationToken ct, string teamId, string teamName,
        string ownerUserId, string? teamAvatarUrl);

    Task<ProjectTeamEntity> GetProjectTeamById(CancellationToken ct, string teamId);

    Task<ProjectTeamEntity?> UpdateProjectTeam(CancellationToken ct, string teamId, string teamName,
        string ownerUserId, string? teamAvatarUrl, string teamDescription);

    Task<ProjectTeamEntity?> DeleteProjectTeamParticipant(CancellationToken ct, string teamId, string userId);
    Task AddProjectTeamParticipant(CancellationToken ct, string teamId, string userId);
}