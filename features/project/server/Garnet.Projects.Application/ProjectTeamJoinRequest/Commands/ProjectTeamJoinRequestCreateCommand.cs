using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Project;
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

    public async Task Execute(CancellationToken ct, string teamId,
        string teamName,
        string projectId)
    {
        var project = await _projectRepository.GetProject(ct, projectId);
        if (project is null)
        {
            return;
        }

        var request = await _projectTeamJoinRequestRepository.AddProjectTeamJoinRequest(ct, teamId, teamName, projectId);
        var notification = request.CreateProjectTeamJoinRequestNotification(project!);
        await _messageBus.Publish(notification);
    }
}