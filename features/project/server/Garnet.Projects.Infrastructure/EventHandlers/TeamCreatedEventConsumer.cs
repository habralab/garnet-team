using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application;
using Garnet.Projects.Events;
using Garnet.Teams.Events;


namespace Garnet.Projects.Infrastructure.EventHandlers
{
    public class TeamCreatedEventConsumer : IMessageBusConsumer<TeamCreatedEventMock>
    {
        private readonly ProjectTeamService _projectTeamService;
        public TeamCreatedEventConsumer(ProjectTeamService projectTeamService)
        {
            _projectTeamService = projectTeamService;
        }

        public async Task Consume(TeamCreatedEventMock message)
        {
            await _projectTeamService.AddProjectTeam(CancellationToken.None, message.Id, message.Name, message.OwnerUserId);
        }
    }

}