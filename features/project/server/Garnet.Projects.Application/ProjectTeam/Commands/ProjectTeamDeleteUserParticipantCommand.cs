namespace Garnet.Projects.Application.ProjectTeam.Commands;

public class ProjectTeamDeleteUserParticipantCommand
{
    private readonly IProjectTeamRepository _projectTeamRepository;

    public ProjectTeamDeleteUserParticipantCommand(IProjectTeamRepository projectTeamRepository)
    {
        _projectTeamRepository = projectTeamRepository;
    }

    public async Task Execute(CancellationToken ct, string teamId, string userId)
    {
        await _projectTeamRepository.DeleteProjectTeamParticipant(ct, teamId, userId);
    }
}