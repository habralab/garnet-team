using Garnet.Projects.Application.ProjectTeam.Args;

namespace Garnet.Projects.Application.ProjectTeam.Commands;

public class ProjectTeamCreateCommand
{
    private readonly IProjectTeamRepository _projectTeamRepository;

    public ProjectTeamCreateCommand(IProjectTeamRepository projectTeamRepository)
    {
        _projectTeamRepository = projectTeamRepository;
    }

    public async Task<ProjectTeamEntity> Execute(CancellationToken ct, ProjectTeamCreateArgs args)
    {
        return await _projectTeamRepository.AddProjectTeam(ct, args.TeamId, args.TeamName, args.OwnerUserId);
    }

}