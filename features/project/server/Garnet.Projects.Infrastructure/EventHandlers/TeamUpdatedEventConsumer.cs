using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application;
using Garnet.Projects.Events;
using Garnet.Teams.Events;

namespace Garnet.Projects.Infrastructure.EventHandlers;

public class TeamUpdatedEventConsumer : IMessageBusConsumer<TeamUpdatedEventMock>
{
    private readonly ProjectTeamParticipantService _projectTeamParticipantService;
    private readonly ProjectTeamService _projectTeamService;

    public TeamUpdatedEventConsumer(ProjectTeamParticipantService projectTeamParticipantService,
        ProjectTeamService projectTeamService)
    {
        _projectTeamParticipantService = projectTeamParticipantService;
        _projectTeamService = projectTeamService;
    }

    public async Task Consume(TeamUpdatedEventMock message)
    {
        await _projectTeamService.UpdateProjectTeam(CancellationToken.None, message.Id, message.Name,
            message.OwnerUserId);

        await _projectTeamParticipantService.UpdateProjectTeamParticipant(CancellationToken.None, message.Id,
            message.Name);
    }
}