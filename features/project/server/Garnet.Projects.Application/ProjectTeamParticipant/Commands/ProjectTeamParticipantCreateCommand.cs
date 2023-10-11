using Garnet.Projects.Application.ProjectTeamParticipant.Args;

namespace Garnet.Projects.Application.ProjectTeamParticipant.Commands;

public class ProjectTeamParticipantCreateCommand
{
    private readonly IProjectTeamParticipantRepository _projectTeamParticipantRepository;

    public ProjectTeamParticipantCreateCommand(IProjectTeamParticipantRepository projectTeamParticipantRepository)
    {
        _projectTeamParticipantRepository = projectTeamParticipantRepository;
    }

    public async Task<ProjectTeamParticipantEntity> Execute(CancellationToken ct, ProjectTeamParticipantCreateArgs args)
    {
        return await _projectTeamParticipantRepository.AddProjectTeamParticipant(ct, args.TeamId, args.TeamName, args.ProjectId);
    }
}