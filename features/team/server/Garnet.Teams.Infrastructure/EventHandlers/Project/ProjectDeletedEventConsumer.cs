using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Events.Project;
using Garnet.Teams.Application.ProjectTeamParticipant;
using Garnet.Teams.Application.TeamJoinProjectRequest;

namespace Garnet.Teams.Infrastructure.EventHandlers.Project
{
    public class ProjectDeletedEventConsumer : IMessageBusConsumer<ProjectDeletedEvent>
    {
        private readonly ITeamJoinProjectRequestRepository _joinProjectRequestRepository;
        private readonly ITeamProjectRepository _teamProjectRepository;

        public ProjectDeletedEventConsumer(ITeamProjectRepository teamProjectRepository, ITeamJoinProjectRequestRepository joinProjectRequestRepository)
        {
            _joinProjectRequestRepository = joinProjectRequestRepository;
            _teamProjectRepository = teamProjectRepository;
        }

        public async Task Consume(ProjectDeletedEvent message)
        {
            await _joinProjectRequestRepository.DeleteJoinProjectRequestByProject(CancellationToken.None, message.ProjectId);
            await _teamProjectRepository.DeleteAllTeamProjectByProject(CancellationToken.None, message.ProjectId);
        }
    }
}