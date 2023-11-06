using FluentResults;
using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.Project.Errors;
using Garnet.Projects.Application.ProjectTeamJoinRequest.Notifications;

namespace Garnet.Projects.Application.ProjectTeamJoinRequest.Commands;

public class ProjectTeamJoinRequestCreateCommand
{
    private readonly IProjectTeamJoinRequestRepository _projectTeamJoinRequestRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMessageBus _messageBus;

    public ProjectTeamJoinRequestCreateCommand(
        IProjectRepository projectRepository,
        IProjectTeamJoinRequestRepository projectTeamJoinRequestRepository,
        IMessageBus messageBus)
    {
        _projectTeamJoinRequestRepository = projectTeamJoinRequestRepository;
        _projectRepository = projectRepository;
        _messageBus = messageBus;
    }

    public async Task<Result<ProjectTeamJoinRequestEntity>> Execute(CancellationToken ct,
        string id,
        string teamId,
        string teamName,
        string projectId)
    {
        var project = await _projectRepository.GetProject(ct, projectId);
        if (project is null)
        {
            return Result.Fail(new ProjectNotFoundError(projectId));
        }

        var request = await _projectTeamJoinRequestRepository.AddProjectTeamJoinRequest(ct, id, teamId, teamName, projectId);
        var notification = request.CreateProjectTeamJoinRequestNotification(project!);
        await _messageBus.Publish(notification);
        return Result.Ok(request);
    }
}