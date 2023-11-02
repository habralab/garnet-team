using Garnet.Projects.Application.ProjectTeamParticipant;

namespace Garnet.Projects.Application.Project.Queries;

public class ProjectFilterByTeamParticipantIdQuery
{
    private readonly IProjectTeamParticipantRepository _projectTeamParticipantRepository;

    public ProjectFilterByTeamParticipantIdQuery(IProjectTeamParticipantRepository projectTeamParticipantRepository)
    {
        _projectTeamParticipantRepository = projectTeamParticipantRepository;
    }

    public async Task<ProjectEntity[]> Query(CancellationToken ct, string teamId)
    {
        return await _projectTeamParticipantRepository.GetProjectsOfTeamParticipantByTeamId(ct, teamId);
    }
}