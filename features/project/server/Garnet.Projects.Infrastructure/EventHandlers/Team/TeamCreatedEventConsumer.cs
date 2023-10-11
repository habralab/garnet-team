using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.ProjectTeam.Args;
using Garnet.Projects.Application.ProjectTeam.Commands;
using Garnet.Teams.Events;

namespace Garnet.Projects.Infrastructure.EventHandlers.Team
{
    public class TeamCreatedEventConsumer : IMessageBusConsumer<TeamCreatedEvent>
    {
        private readonly ProjectTeamCreateCommand _projectTeamCreateCommand;

        public TeamCreatedEventConsumer(ProjectTeamCreateCommand projectTeamCreateCommand)
        {
            _projectTeamCreateCommand = projectTeamCreateCommand;
        }

        public async Task Consume(TeamCreatedEvent message)
        {
            var args = new ProjectTeamCreateArgs(message.Id, message.Name, message.OwnerUserId);
            await _projectTeamCreateCommand.Execute(CancellationToken.None, args);
        }
    }
}