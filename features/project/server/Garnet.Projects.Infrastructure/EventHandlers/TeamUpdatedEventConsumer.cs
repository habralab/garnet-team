using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application;
using Garnet.Projects.Events;
using Garnet.Teams.Events;

namespace Garnet.Projects.Infrastructure.EventHandlers;

public class TeamUpdatedEventConsumer : IMessageBusConsumer<TeamUpdatedEventMock>
{
    private readonly ProjectTeamParticipantService _projectTeamParticipantService;
    private readonly ProjectTeamService _projectTeamService;
    private readonly ProjectTeamJoinRequestService _projectTeamJoinRequestService;

    public TeamUpdatedEventConsumer(ProjectTeamParticipantService projectTeamParticipantService,
        ProjectTeamService projectTeamService, ProjectTeamJoinRequestService projectTeamJoinRequestService)
    {
        _projectTeamParticipantService = projectTeamParticipantService;
        _projectTeamService = projectTeamService;
        _projectTeamJoinRequestService = projectTeamJoinRequestService;
    }

    public async Task Consume(TeamUpdatedEventMock message)
    {
        await _projectTeamService.UpdateProjectTeam(CancellationToken.None, message.Id, message.Name,
            message.OwnerUserId);

        await _projectTeamParticipantService.UpdateProjectTeamParticipant(CancellationToken.None, message.Id,
            message.Name);
        await _projectTeamJoinRequestService.UpdateProjectTeamJoinRequest(CancellationToken.None, message.Id,
            message.Name);
    }
}