using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application;
using Garnet.Projects.Events;
using Garnet.Teams.Events;


namespace Garnet.Projects.Infrastructure.EventHandlers
{
    public class TeamCreatedEventConsumer : IMessageBusConsumer<TeamCreatedEventMock>
    {
        private readonly ProjectTeamParticipantService _projectTeamParticipantService;
        public TeamCreatedEventConsumer(ProjectTeamParticipantService projectTeamParticipantService)
        {
            _projectTeamParticipantService = projectTeamParticipantService;
        }

        public async Task Consume(TeamCreatedEventMock message)
        {
            await _projectTeamParticipantService.AddProjectTeamParticipant(CancellationToken.None, message.Id, message.Name, null);
        }
    }

}