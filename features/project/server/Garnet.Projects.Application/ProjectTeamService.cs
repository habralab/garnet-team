using FluentResults;

namespace Garnet.Projects.Application;

public class ProjectTeamService
{
    private readonly IProjectTeamRepository _repository;

    public ProjectTeamService(IProjectTeamRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProjectTeam> AddProjectTeam(CancellationToken ct, string teamId,
        string teamName, string ownerUSerId)
    {
        return await _repository.AddProjectTeam(ct, teamId, teamName, ownerUSerId);
    }

    public async Task<ProjectTeam> UpdateProjectTeam(CancellationToken ct, string teamId,
        string teamName, string ownerUserId)
    {
        return await _repository.UpdateProjectTeam(ct, teamId, teamName, ownerUserId);
    }
}