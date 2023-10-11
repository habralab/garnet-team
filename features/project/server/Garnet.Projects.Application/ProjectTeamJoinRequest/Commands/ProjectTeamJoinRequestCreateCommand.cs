namespace Garnet.Projects.Application.ProjectTeamJoinRequest.Commands;

public class ProjectTeamJoinRequestCreateCommand
{
    private readonly IProjectTeamJoinRequestRepository _projectTeamJoinRequestRepository;

    public ProjectTeamJoinRequestCreateCommand(IProjectTeamJoinRequestRepository projectTeamJoinRequestRepository)
    {
        _projectTeamJoinRequestRepository = projectTeamJoinRequestRepository;
    }

    public async Task<ProjectTeamJoinRequestEntity> Execute(CancellationToken ct, string teamId,
        string teamName,
        string projectId)
    {
        return await _projectTeamJoinRequestRepository.AddProjectTeamJoinRequest(ct, teamId, teamName, projectId);
    }
}