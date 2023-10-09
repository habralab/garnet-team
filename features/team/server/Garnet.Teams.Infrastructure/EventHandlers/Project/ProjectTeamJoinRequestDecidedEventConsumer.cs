using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Events;
using Garnet.Teams.Application;
using Garnet.Teams.Application.TeamJoinProjectRequest;

namespace Garnet.Teams.Infrastructure.EventHandlers.Project
{
    public class ProjectTeamJoinRequestDecidedEventConsumer : IMessageBusConsumer<ProjectTeamJoinRequestDecidedEvent>
    {
        private readonly ITeamJoinProjectRequestRepository _joinProjectRequestRepository;

        public ProjectTeamJoinRequestDecidedEventConsumer(ITeamJoinProjectRequestRepository joinProjectRequestRepository)
        {
            _joinProjectRequestRepository = joinProjectRequestRepository;
        }

        public async Task Consume(ProjectTeamJoinRequestDecidedEvent message)
        {
            await _joinProjectRequestRepository.DeleteJoinProjectRequestById(CancellationToken.None, message.Id);
        }
    }
}