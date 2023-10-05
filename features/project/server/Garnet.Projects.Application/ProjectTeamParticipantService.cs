using FluentResults;

namespace Garnet.Projects.Application;

public class ProjectTeamParticipantService
{
    private readonly IProjectTeamParticipantRepository _repository;

    public ProjectTeamParticipantService(IProjectTeamParticipantRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProjectTeamParticipant> AddProjectTeamParticipant(CancellationToken ct, string teamId,
        string teamName, string? projectId)
    {
        return await _repository.AddProjectTeamParticipant(ct, teamId, teamName, projectId);
    }

    public async Task<ProjectTeamParticipant[]> GetProjectTeamParticipantByProjectId(CancellationToken ct,
        string teamId)
    {
        return await _repository.GetProjectTeamParticipantsByProjectId(ct, teamId);
    }

    public async Task<ProjectTeamParticipant[]> UpdateProjectTeamParticipant(CancellationToken ct, string teamId,
        string teamName)
    {
        return await _repository.UpdateProjectTeamParticipant(ct, teamId, teamName);
    }
}