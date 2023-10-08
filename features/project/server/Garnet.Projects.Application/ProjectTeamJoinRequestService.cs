using FluentResults;
using Garnet.Common.Application;
using Garnet.Projects.Application.Errors;

namespace Garnet.Projects.Application;

public class ProjectTeamJoinRequestService
{
    private readonly ProjectService _projectService;
    private readonly IProjectTeamJoinRequestRepository _projectTeamJoinRequestRepository;

    public ProjectTeamJoinRequestService(ProjectService projectService,
        IProjectTeamJoinRequestRepository projectTeamJoinRequestRepository)
    {
        _projectService = projectService;
        _projectTeamJoinRequestRepository = projectTeamJoinRequestRepository;
    }

    public async Task<ProjectTeamJoinRequest> AddProjectTeamJoinRequest(CancellationToken ct, string teamId,
        string teamName,
        string projectId)
    {
        return await _projectTeamJoinRequestRepository.AddProjectTeamJoinRequest(ct, teamId, teamName, projectId);
    }

    public async Task<Result<ProjectTeamJoinRequest[]>> GetProjectTeamJoinRequestsByProjectId(CancellationToken ct,
        ICurrentUserProvider currentUserProvider, string projectId)
    {
        var project = await _projectService.GetProject(ct, projectId);
        if (project.IsFailed)
        {
            return Result.Fail(new ProjectNotFoundError(projectId));
        }

        var projectObj = project.Value;
        if (projectObj?.OwnerUserId != currentUserProvider.UserId)
        {
            return Result.Fail(new ProjectOnlyOwnerCanGetTeamJoinRequestsError());
        }

        return await _projectTeamJoinRequestRepository.GetProjectTeamJoinRequestsByProjectId(ct, projectId);
    }

    public async Task UpdateProjectTeamJoinRequest(CancellationToken ct, string teamId, string teamName)
    {
        await _projectTeamJoinRequestRepository.UpdateProjectTeamJoinRequest(ct, teamId, teamName);
    }
}