using Garnet.Projects.Application.ProjectTeamParticipant.Args;

namespace Garnet.Projects.Application.ProjectTeamParticipant.Commands;

public class ProjectTeamParticipantUpdateCommand
{
    private readonly IProjectTeamParticipantRepository _projectTeamParticipantRepository;

    public ProjectTeamParticipantUpdateCommand(IProjectTeamParticipantRepository projectTeamParticipantRepository)
    {
        _projectTeamParticipantRepository = projectTeamParticipantRepository;
    }

    public async Task Execute(CancellationToken ct, ProjectTeamParticipantUpdateArgs args)
    {
        await _projectTeamParticipantRepository.UpdateProjectTeamParticipant(ct, args.TeamId, args.TeamName);
    }
}