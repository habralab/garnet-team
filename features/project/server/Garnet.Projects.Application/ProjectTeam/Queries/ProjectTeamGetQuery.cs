namespace Garnet.Projects.Application.ProjectTeam.Queries;

public class ProjectTeamGetQuery
{
    private readonly IProjectTeamRepository _projectTeamRepository;

    public ProjectTeamGetQuery(IProjectTeamRepository projectTeamRepository)
    {
        _projectTeamRepository = projectTeamRepository;
    }

    public async Task<ProjectTeamEntity> Query(CancellationToken ct, string teamId)
    {
        return await _projectTeamRepository.GetProjectTeamById(ct, teamId);
    }
}