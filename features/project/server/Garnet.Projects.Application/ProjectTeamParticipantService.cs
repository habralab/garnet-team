using FluentResults;

namespace Garnet.Projects.Application;

public class ProjectTeamParticipantService
{
    private readonly IProjectTeamParticipantRepository _repository;

    public ProjectTeamParticipantService(IProjectTeamParticipantRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<ProjectTeamParticipant>> AddProjectTeamParticipant(CancellationToken ct, string id, string teamId, string projectId)
    {
        return await _repository.AddProjectTeamParticipant(ct, id, teamId, projectId);
    }
    public async Task<Result<ProjectTeamParticipant[]>> GetProjectTeamParticipantByProjectId(CancellationToken ct, string teamId)
    {
        return await _repository.GetProjectTeamParticipantByProjectId(ct, teamId);
    }
}