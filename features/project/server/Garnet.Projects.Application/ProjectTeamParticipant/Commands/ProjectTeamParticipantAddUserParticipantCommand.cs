using Garnet.Projects.Application.ProjectTeamParticipant.Args;

namespace Garnet.Projects.Application.ProjectTeamParticipant.Commands;

public class ProjectTeamParticipantAddUserParticipantCommand
{
    private readonly IProjectTeamParticipantRepository _projectTeamParticipantRepository;

    public ProjectTeamParticipantAddUserParticipantCommand(IProjectTeamParticipantRepository projectTeamParticipantRepository)
    {
        _projectTeamParticipantRepository = projectTeamParticipantRepository;
    }

    public async Task Execute(CancellationToken ct, ProjectTeamParticipantAddParticipantArgs args)
    {
        await _projectTeamParticipantRepository.AddProjectTeamUserParticipant(ct, args.TeamId, args.UserId);
    }
}