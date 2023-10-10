using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application;
using Garnet.Teams.Events;

namespace Garnet.Projects.Infrastructure.EventHandlers.Team
{
    public class TeamCreatedEventConsumer : IMessageBusConsumer<TeamCreatedEvent>
    {
        private readonly ProjectTeamService _projectTeamService;
        public TeamCreatedEventConsumer(ProjectTeamService projectTeamService)
        {
            _projectTeamService = projectTeamService;
        }

        public async Task Consume(TeamCreatedEvent message)
        {
            await _projectTeamService.AddProjectTeam(CancellationToken.None, message.Id, message.Name, message.OwnerUserId);
        }
    }

}