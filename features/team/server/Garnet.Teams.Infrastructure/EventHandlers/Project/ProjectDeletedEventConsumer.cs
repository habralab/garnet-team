using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Events;
using Garnet.Teams.Application.TeamJoinProjectRequest;

namespace Garnet.Teams.Infrastructure.EventHandlers.Project
{
    public class ProjectDeletedEventConsumer : IMessageBusConsumer<ProjectDeletedEvent>
    {
        private readonly ITeamJoinProjectRequestRepository _joinProjectRequestRepository;

        public ProjectDeletedEventConsumer(ITeamJoinProjectRequestRepository joinProjectRequestRepository)
        {
            _joinProjectRequestRepository = joinProjectRequestRepository;
        }

        public async Task Consume(ProjectDeletedEvent message)
        {
            await _joinProjectRequestRepository.DeleteJoinProjectRequestByProject(CancellationToken.None, message.ProjectId);
        }
    }
}