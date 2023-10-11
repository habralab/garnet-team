using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Events.Project;
using Garnet.Teams.Application;

namespace Garnet.Teams.Infrastructure.EventHandlers
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