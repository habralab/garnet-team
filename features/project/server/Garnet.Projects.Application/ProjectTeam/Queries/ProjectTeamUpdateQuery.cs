using Garnet.Projects.Application.ProjectTeam.Args;

namespace Garnet.Projects.Application.ProjectTeam.Queries;

public class ProjectTeamUpdateQuery
{
    private readonly IProjectTeamRepository _projectTeamRepository;

    public ProjectTeamUpdateQuery(IProjectTeamRepository projectTeamRepository)
    {
        _projectTeamRepository = projectTeamRepository;
    }

    public async Task<ProjectTeamEntity> Query(CancellationToken ct, ProjectTeamUpdateArgs args)
    {
        return await _projectTeamRepository.UpdateProjectTeam(ct, args.TeamId, args.TeamName, args.OwnerUserId);
    }
}