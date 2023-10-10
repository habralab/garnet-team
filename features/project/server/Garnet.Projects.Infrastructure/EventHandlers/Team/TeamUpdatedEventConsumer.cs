using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application;
using Garnet.Teams.Events;

namespace Garnet.Projects.Infrastructure.EventHandlers.Team;

public class TeamUpdatedEventConsumer : IMessageBusConsumer<TeamUpdatedEvent>
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

    public async Task Consume(TeamUpdatedEvent message)
    {
        await _projectTeamService.UpdateProjectTeam(CancellationToken.None, message.Id, message.Name,
            message.OwnerUserId);

        await _projectTeamParticipantService.UpdateProjectTeamParticipant(CancellationToken.None, message.Id,
            message.Name);
        await _projectTeamJoinRequestService.UpdateProjectTeamJoinRequest(CancellationToken.None, message.Id,
            message.Name);
    }
}