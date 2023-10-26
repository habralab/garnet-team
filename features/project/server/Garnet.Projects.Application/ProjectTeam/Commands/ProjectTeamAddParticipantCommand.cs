using Garnet.Projects.Application.ProjectTeam.Args;

namespace Garnet.Projects.Application.ProjectTeam.Commands;

public class ProjectTeamAddParticipantCommand
{
    private readonly IProjectTeamRepository _projectTeamRepository;

    public ProjectTeamAddParticipantCommand(IProjectTeamRepository projectTeamRepository)
    {
        _projectTeamRepository = projectTeamRepository;
    }

    public async Task Execute(CancellationToken ct, ProjectTeamAddParticipantArgs args)
    {
        await _projectTeamRepository.AddProjectTeamParticipant(ct, args.TeamId, args.UserId);
    }
}