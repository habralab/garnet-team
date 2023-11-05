using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Events.Project;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.TeamJoinProjectRequest;

namespace Garnet.Teams.Infrastructure.EventHandlers.Project
{
    public class ProjectDeletedEventConsumer : IMessageBusConsumer<ProjectDeletedEvent>
    {
        private readonly ITeamJoinProjectRequestRepository _joinProjectRequestRepository;
        private readonly ITeamRepository _teamRepository;

        public ProjectDeletedEventConsumer(ITeamRepository teamRepository, ITeamJoinProjectRequestRepository joinProjectRequestRepository)
        {
            _teamRepository = teamRepository;
            _joinProjectRequestRepository = joinProjectRequestRepository;
        }

        public async Task Consume(ProjectDeletedEvent message)
        {
            await _joinProjectRequestRepository.DeleteJoinProjectRequestByProject(CancellationToken.None, message.ProjectId);
            await _teamRepository.RemoveProjectInAllTeams(CancellationToken.None, message.ProjectId);
        }
    }
}