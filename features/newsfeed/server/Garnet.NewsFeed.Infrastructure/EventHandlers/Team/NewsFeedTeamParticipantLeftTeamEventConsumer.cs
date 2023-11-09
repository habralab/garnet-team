using Garnet.Common.Application.MessageBus;
using Garnet.NewsFeed.Application.NewsFeedTeamParticipant;
using Garnet.Teams.Events.TeamParticipant;

namespace Garnet.NewsFeed.Infrastructure.EventHandlers.Team
{
    public class NewsFeedTeamParticipantLeftTeamEventConsumer : IMessageBusConsumer<TeamParticipantLeftTeamEvent>
    {
        private readonly INewsFeedTeamParticipantRepository _newsFeedTeamParticipantRepository;

        public NewsFeedTeamParticipantLeftTeamEventConsumer(INewsFeedTeamParticipantRepository newsFeedTeamParticipantRepository)
        {
            _newsFeedTeamParticipantRepository = newsFeedTeamParticipantRepository;
        }

        public async Task Consume(TeamParticipantLeftTeamEvent message)
        {
            await _newsFeedTeamParticipantRepository.DeleteTeamParticipantById(message.UserId);
        }
    }
}