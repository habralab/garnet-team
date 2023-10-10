using Garnet.Projects.Application.ProjectTeamParticipant;

namespace Garnet.Projects.Application;

public class ProjectTeamParticipantService
{
    private readonly IProjectTeamParticipantRepository _repository;

    public ProjectTeamParticipantService(IProjectTeamParticipantRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProjectTeamParticipantEntity> AddProjectTeamParticipant(CancellationToken ct, string teamId,
        string teamName, string projectId)
    {
        return await _repository.AddProjectTeamParticipant(ct, teamId, teamName, projectId);
    }

    public async Task<ProjectTeamParticipantEntity[]> GetProjectTeamParticipantByProjectId(CancellationToken ct,
        string teamId)
    {
        return await _repository.GetProjectTeamParticipantsByProjectId(ct, teamId);
    }

    public async Task UpdateProjectTeamParticipant(CancellationToken ct, string teamId,
        string teamName)
    {
        await _repository.UpdateProjectTeamParticipant(ct, teamId, teamName);
    }
}