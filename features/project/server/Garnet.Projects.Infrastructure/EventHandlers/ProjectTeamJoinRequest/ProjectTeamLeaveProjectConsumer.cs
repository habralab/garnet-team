using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Events.TeamJoinProjectRequest;

namespace Garnet.Projects.Infrastructure.EventHandlers.ProjectTeamJoinRequest
{
    public class ProjectTeamLeaveProjectConsumer : IMessageBusConsumer<TeamLeaveProjectEvent>
    {
        public ProjectTeamLeaveProjectConsumer()
        {
            
        }

        public Task Consume(TeamLeaveProjectEvent message)
        {
            throw new NotImplementedException();
        }
    }
}