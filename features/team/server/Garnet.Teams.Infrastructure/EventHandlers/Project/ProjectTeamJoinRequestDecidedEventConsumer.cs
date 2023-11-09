using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Events.ProjectTeamJoinRequest;
using Garnet.Teams.Application.TeamJoinProjectRequest;
using Garnet.Teams.Application.TeamJoinProjectRequest.Commands;

namespace Garnet.Teams.Infrastructure.EventHandlers.Project
{
    public class ProjectTeamJoinRequestDecidedEventConsumer : IMessageBusConsumer<ProjectTeamJoinRequestDecidedEvent>
    {
        private readonly TeamJoinProjectRequestDecidedCommand _teamJoinProjectRequestDecidedCommand;

        public ProjectTeamJoinRequestDecidedEventConsumer(TeamJoinProjectRequestDecidedCommand teamJoinProjectRequestDecidedCommand)
        {
            _teamJoinProjectRequestDecidedCommand = teamJoinProjectRequestDecidedCommand;
        }

        public async Task Consume(ProjectTeamJoinRequestDecidedEvent message)
        {
            await _teamJoinProjectRequestDecidedCommand.Execute(CancellationToken.None, message.Id, message.IsApproved);
        }
    }
}