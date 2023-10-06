using FluentResults;

namespace Garnet.Projects.Application;

public class ProjectTeamJoinRequestService
{
    private readonly IProjectTeamJoinRequestRepository _repository;

    public ProjectTeamJoinRequestService(IProjectTeamJoinRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProjectTeamJoinRequest> AddProjectTeamJoinRequest(CancellationToken ct, string teamId, string teamName,
        string projectId)
    {
        return await _repository.AddProjectTeamJoinRequest(ct, teamId, teamName, projectId);
    }

    public async Task UpdateProjectTeamJoinRequest(CancellationToken ct, string teamId, string teamName)
    {
        await _repository.UpdateProjectTeamJoinRequest(ct, teamId, teamName);
    }
}