using FluentResults;
using Garnet.Common.Application;
using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.Project.Errors;
using Garnet.Projects.Application.ProjectTeamJoinRequest.Errors;

namespace Garnet.Projects.Application.ProjectTeamJoinRequest.Queries;

public class ProjectTeamJoinRequestFilterQuery
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectTeamJoinRequestRepository _projectTeamJoinRequestRepository;


    public ProjectTeamJoinRequestFilterQuery(
        ICurrentUserProvider currentUserProvider,
        IProjectRepository projectRepository,
        IProjectTeamJoinRequestRepository projectTeamJoinRequestRepository)
    {
        _currentUserProvider = currentUserProvider;
        _projectRepository = projectRepository;
        _projectTeamJoinRequestRepository = projectTeamJoinRequestRepository;
    }

    public async Task<Result<ProjectTeamJoinRequestEntity[]>> Query(CancellationToken ct, string projectId)
    {
        var project = await _projectRepository.GetProject(ct, projectId);
        if (project is null)
        {
            return Result.Fail(new ProjectNotFoundError(projectId));
        }

        if (project.OwnerUserId != _currentUserProvider.UserId)
        {
            return Result.Fail(new ProjectOnlyOwnerCanGetTeamJoinRequestsError());
        }

        return await _projectTeamJoinRequestRepository.GetProjectTeamJoinRequestsByProjectId(ct, projectId);
    }
}