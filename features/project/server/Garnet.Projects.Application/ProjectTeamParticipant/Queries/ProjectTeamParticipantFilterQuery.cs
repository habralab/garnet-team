namespace Garnet.Projects.Application.ProjectTeamParticipant.Queries;

public class ProjectTeamParticipantFilterQuery
{
    private readonly IProjectTeamParticipantRepository _projectTeamParticipantRepository;

    public ProjectTeamParticipantFilterQuery(IProjectTeamParticipantRepository projectTeamParticipantRepository)
    {
        _projectTeamParticipantRepository = projectTeamParticipantRepository;
    }

    public async Task<ProjectTeamParticipantEntity[]> Query(CancellationToken ct, string teamId)
    {
        return await _projectTeamParticipantRepository.GetProjectTeamParticipantsByProjectId(ct, teamId);
    }
}