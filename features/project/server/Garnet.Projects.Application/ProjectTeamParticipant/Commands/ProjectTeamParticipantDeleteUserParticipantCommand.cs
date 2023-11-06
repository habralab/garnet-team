using Garnet.Projects.Application.ProjectTeamParticipant.Args;

namespace Garnet.Projects.Application.ProjectTeamParticipant.Commands;

public class ProjectTeamParticipantDeleteUserParticipantCommand
{
    private readonly IProjectTeamParticipantRepository _projectTeamParticipantRepository;

    public ProjectTeamParticipantDeleteUserParticipantCommand(IProjectTeamParticipantRepository projectTeamParticipantRepository)
    {
        _projectTeamParticipantRepository = projectTeamParticipantRepository;
    }

    public async Task Execute(CancellationToken ct, string teamId, string userId)
    {
        await _projectTeamParticipantRepository.DeleteProjectTeamUserParticipant(ct, teamId, userId);
    }
}