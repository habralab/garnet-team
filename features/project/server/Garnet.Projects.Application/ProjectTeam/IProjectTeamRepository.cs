namespace Garnet.Projects.Application;

public interface IProjectTeamRepository
{
    Task<ProjectTeam> AddProjectTeam(CancellationToken ct, string teamId, string teamName,
        string ownerUserId);

    Task<ProjectTeam> UpdateProjectTeam(CancellationToken ct, string teamId, string teamName,
        string ownerUserId);
}