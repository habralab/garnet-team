using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application;
using Garnet.Projects.Events;

namespace Garnet.Projects.Infrastructure.EventHandlers;

public class ProjectTeamJoinRequestCreatedConsumer : IMessageBusConsumer<TeamJoinRequestCreatedEventMock>
{
    private readonly ProjectTeamJoinRequestService _projectTeamJoinRequestService;
    public ProjectTeamJoinRequestCreatedConsumer(ProjectTeamJoinRequestService projectTeamJoinRequestService)
    {
        _projectTeamJoinRequestService = projectTeamJoinRequestService;
    }

    public async Task Consume(TeamJoinRequestCreatedEventMock message)
    {
        await _projectTeamJoinRequestService.AddProjectTeamJoinRequest(CancellationToken.None, message.TeamId, message.TeamName, message.ProjectId);
    }
}