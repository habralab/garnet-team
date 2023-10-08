using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Errors;
using Garnet.Projects.Events;

namespace Garnet.Projects.Application;

public class ProjectTeamJoinRequestService
{
    private readonly ProjectService _projectService;
    private readonly IProjectTeamJoinRequestRepository _projectTeamJoinRequestRepository;
    private ProjectTeamParticipantService _projectTeamParticipantService;
    private readonly IMessageBus _messageBus;

    public ProjectTeamJoinRequestService(ProjectService projectService,
        IProjectTeamJoinRequestRepository projectTeamJoinRequestRepository,
        ProjectTeamParticipantService projectTeamParticipantService, IMessageBus messageBus)
    {
        _projectService = projectService;
        _projectTeamJoinRequestRepository = projectTeamJoinRequestRepository;
        _projectTeamParticipantService = projectTeamParticipantService;
        _messageBus = messageBus;
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

    public async Task<Result<ProjectTeamJoinRequest>> ProjectTeamJoinRequestDecide(CancellationToken ct,
        ICurrentUserProvider currentUserProvider, string teamJoinRequestId, bool isApproved)
    {
        var teamJoinRequest =
            await _projectTeamJoinRequestRepository.GetProjectTeamJoinRequestById(ct, teamJoinRequestId);
        if (teamJoinRequest is null)
        {
            return Result.Fail(new ProjectTeamJoinRequestNotFoundError(teamJoinRequestId));
        }

        var findProject = await _projectService.GetProject(ct, teamJoinRequest.ProjectId);
        if (findProject.IsFailed)
        {
            return Result.Fail(findProject.Errors);
        }

        var project = findProject.Value;
        if (project.OwnerUserId != currentUserProvider.UserId)
        {
            return Result.Fail(new ProjectTeamJoinRequestOnlyOwnerCanDecideError());
        }

        if (isApproved)
        {
            await _projectTeamParticipantService.AddProjectTeamParticipant(ct, teamJoinRequest.TeamId,
                teamJoinRequest.TeamName, teamJoinRequest.ProjectId);
        }

        await _projectTeamJoinRequestRepository.DeleteProjectTeamJoinRequestById(ct, teamJoinRequestId);

        var decideEvent = new ProjectTeamJoinRequestDecidedEvent(teamJoinRequest.Id, teamJoinRequest.TeamId,
            teamJoinRequest.ProjectId, isApproved);
        await _messageBus.Publish(decideEvent);
        return Result.Ok(teamJoinRequest);
    }
}