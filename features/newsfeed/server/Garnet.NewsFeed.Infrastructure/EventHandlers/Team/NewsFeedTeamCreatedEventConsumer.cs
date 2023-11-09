using Garnet.Common.Application.MessageBus;
using Garnet.Common.Infrastructure.Support;
using Garnet.NewsFeed.Application.NewsFeedTeam;
using Garnet.NewsFeed.Application.NewsFeedTeamParticipant;
using Garnet.Teams.Events.Team;

namespace Garnet.NewsFeed.Infrastructure.EventHandlers.Team
{
    public class NewsFeedTeamCreatedEventConsumer : IMessageBusConsumer<TeamCreatedEvent>
    {
        private readonly INewsFeedTeamRepository _newsFeedTeamRepository;
        private readonly INewsFeedTeamParticipantRepository _newsFeedTeamParticipantRepository;

        public NewsFeedTeamCreatedEventConsumer(
            INewsFeedTeamRepository newsFeedTeamRepository,
            INewsFeedTeamParticipantRepository newsFeedTeamParticipantRepository)
        {
            _newsFeedTeamParticipantRepository = newsFeedTeamParticipantRepository;
            _newsFeedTeamRepository = newsFeedTeamRepository;
        }

        public async Task Consume(TeamCreatedEvent message)
        {
            await _newsFeedTeamRepository.CreateTeam(message.Id, message.OwnerUserId);
            await _newsFeedTeamParticipantRepository.CreateTeamParticipant(Uuid.NewGuid(), message.Id, message.OwnerUserId);
        }
    }
}