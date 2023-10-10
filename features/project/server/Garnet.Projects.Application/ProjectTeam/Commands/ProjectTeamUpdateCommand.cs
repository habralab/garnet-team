using Garnet.Projects.Application.ProjectTeam.Args;

namespace Garnet.Projects.Application.ProjectTeam.Commands;

public class ProjectTeamUpdateCommand
{
    private readonly IProjectTeamRepository _projectTeamRepository;

    public ProjectTeamUpdateCommand(IProjectTeamRepository projectTeamRepository)
    {
        _projectTeamRepository = projectTeamRepository;
    }

    public async Task<ProjectTeamEntity> Execute(CancellationToken ct, ProjectTeamUpdateArgs args)
    {
        return await _projectTeamRepository.UpdateProjectTeam(ct, args.TeamId, args.TeamName, args.OwnerUserId);
    }
}