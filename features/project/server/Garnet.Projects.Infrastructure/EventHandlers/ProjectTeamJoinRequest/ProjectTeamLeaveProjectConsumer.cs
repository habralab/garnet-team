using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.ProjectTeamParticipant.Commands;
using Garnet.Teams.Events.TeamJoinProjectRequest;

namespace Garnet.Projects.Infrastructure.EventHandlers.ProjectTeamJoinRequest
{
    public class ProjectTeamLeaveProjectConsumer : IMessageBusConsumer<TeamLeaveProjectEvent>
    {
        private readonly ProjectTeamParticipantLeaveCommand _projectTeamParticipantDeleteCommand;
        public ProjectTeamLeaveProjectConsumer(ProjectTeamParticipantLeaveCommand projectTeamParticipantDeleteCommand)
        {
            _projectTeamParticipantDeleteCommand = projectTeamParticipantDeleteCommand;
        }

        public async Task Consume(TeamLeaveProjectEvent message)
        {
            await _projectTeamParticipantDeleteCommand.Execute(CancellationToken.None, message.TeamId, message.ProjectId);
        }
    }
}