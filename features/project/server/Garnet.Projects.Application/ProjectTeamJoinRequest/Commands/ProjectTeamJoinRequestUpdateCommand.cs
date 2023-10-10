using Garnet.Projects.Application.ProjectTeamJoinRequest.Args;

namespace Garnet.Projects.Application.ProjectTeamJoinRequest.Commands;

public class ProjectTeamJoinRequestUpdateCommand
{
    private readonly IProjectTeamJoinRequestRepository _projectTeamJoinRequestRepository;

    public ProjectTeamJoinRequestUpdateCommand(IProjectTeamJoinRequestRepository projectTeamJoinRequestRepository)
    {
        _projectTeamJoinRequestRepository = projectTeamJoinRequestRepository;
    }

    public async Task Execute(CancellationToken ct, ProjectTeamJoinRequestUpdateArgs args)
    {
        await _projectTeamJoinRequestRepository.UpdateProjectTeamJoinRequest(ct, args.TeamId, args.TeamName);
    }
}