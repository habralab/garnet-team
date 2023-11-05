using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Events.ProjectTeamJoinRequest;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.TeamJoinProjectRequest;

namespace Garnet.Teams.Infrastructure.EventHandlers.Project
{
    public class ProjectTeamJoinRequestDecidedEventConsumer : IMessageBusConsumer<ProjectTeamJoinRequestDecidedEvent>
    {
        private readonly ITeamJoinProjectRequestRepository _joinProjectRequestRepository;
        private readonly ITeamRepository _teamRepository;

        public ProjectTeamJoinRequestDecidedEventConsumer(
            ITeamRepository teamRepository,
            ITeamJoinProjectRequestRepository joinProjectRequestRepository)
        {
            _teamRepository = teamRepository;
            _joinProjectRequestRepository = joinProjectRequestRepository;
        }

        public async Task Consume(ProjectTeamJoinRequestDecidedEvent message)
        {
            await _joinProjectRequestRepository.DeleteJoinProjectRequestById(CancellationToken.None, message.Id);

            if (message.IsApproved)
            {
                await _teamRepository.AddProjectId(CancellationToken.None, message.TeamId, message.ProjectId);
            }
        }
    }
}