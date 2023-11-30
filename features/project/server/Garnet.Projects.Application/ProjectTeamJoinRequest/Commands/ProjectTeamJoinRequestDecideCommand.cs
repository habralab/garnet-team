using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.Project.Errors;
using Garnet.Projects.Application.ProjectTeam;
using Garnet.Projects.Application.ProjectTeamJoinRequest.Errors;
using Garnet.Projects.Application.ProjectTeamJoinRequest.Notifications;
using Garnet.Projects.Application.ProjectTeamParticipant;
using Garnet.Projects.Events.ProjectTeamJoinRequest;

namespace Garnet.Projects.Application.ProjectTeamJoinRequest.Commands;

public class ProjectTeamJoinRequestDecideCommand
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectTeamJoinRequestRepository _projectTeamJoinRequestRepository;
    private readonly IProjectTeamParticipantRepository _projectTeamParticipantRepository;
    private readonly IMessageBus _messageBus;
    private readonly IProjectTeamRepository _projectTeamRepository;

    public ProjectTeamJoinRequestDecideCommand(
        ICurrentUserProvider currentUserProvider,
        IProjectRepository projectRepository,
        IProjectTeamRepository projectTeamRepository,
        IProjectTeamJoinRequestRepository projectTeamJoinRequestRepository,
        IProjectTeamParticipantRepository projectTeamParticipantRepository,
        IMessageBus messageBus)
    {
        _currentUserProvider = currentUserProvider;
        _projectRepository = projectRepository;
        _projectTeamRepository = projectTeamRepository;
        _projectTeamJoinRequestRepository = projectTeamJoinRequestRepository;
        _projectTeamParticipantRepository = projectTeamParticipantRepository;
        _messageBus = messageBus;
    }

    public async Task<Result<ProjectTeamJoinRequestEntity>> Execute(CancellationToken ct, string teamJoinRequestId, bool isApproved)
    {
        var teamJoinRequest =
            await _projectTeamJoinRequestRepository.GetProjectTeamJoinRequestById(ct, teamJoinRequestId);
        if (teamJoinRequest is null)
        {
            return Result.Fail(new ProjectTeamJoinRequestNotFoundError(teamJoinRequestId));
        }

        var project = await _projectRepository.GetProject(ct, teamJoinRequest.ProjectId);
        if (project is null)
        {
            return Result.Fail(new ProjectNotFoundError(teamJoinRequest.ProjectId));
        }

        if (project.OwnerUserId != _currentUserProvider.UserId)
        {
            return Result.Fail(new ProjectTeamJoinRequestOnlyOwnerCanDecideError());
        }

        if (isApproved)
        {
            await _projectTeamParticipantRepository.AddProjectTeamParticipant(ct, teamJoinRequest.TeamId,
                teamJoinRequest.TeamName, teamJoinRequest.ProjectId);
        }

        await _projectTeamJoinRequestRepository.DeleteProjectTeamJoinRequestById(ct, teamJoinRequestId);

        var decideEvent = new ProjectTeamJoinRequestDecidedEvent(teamJoinRequest.Id, teamJoinRequest.TeamId,
            teamJoinRequest.ProjectId, isApproved);
        await _messageBus.Publish(decideEvent);

        var notificationForDelete = teamJoinRequest.DeleteProjectTeamJoinRequestNotification(project.OwnerUserId);
        await _messageBus.Publish(notificationForDelete);

        var team = await _projectTeamRepository.GetProjectTeamById(ct, teamJoinRequest.TeamId);
        var notification = teamJoinRequest.CreateProjectTeamJoinRequestDecideNotification(project, team, isApproved);
        await _messageBus.Publish(notification);
        return Result.Ok(teamJoinRequest);
    }
}