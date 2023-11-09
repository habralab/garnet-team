using Garnet.Common.Application.MessageBus;
using Garnet.NewsFeed.Application.NewsFeedPost;
using Garnet.NewsFeed.Application.NewsFeedTeam;
using Garnet.NewsFeed.Application.NewsFeedTeamParticipant;
using Garnet.Teams.Events.Team;

namespace Garnet.NewsFeed.Infrastructure.EventHandlers.Team
{
    public class NewsFeedTeamDeletedEventConsumer : IMessageBusConsumer<TeamDeletedEvent>
    {
        private readonly INewsFeedTeamRepository _newsFeedTeamRepository;
        private readonly INewsFeedPostRepository _newsFeedPostRepository;
        private readonly INewsFeedTeamParticipantRepository _newsFeedTeamParticipantRepository;

        public NewsFeedTeamDeletedEventConsumer(
            INewsFeedTeamRepository newsFeedTeamRepository,
            INewsFeedPostRepository newsFeedPostRepository,
            INewsFeedTeamParticipantRepository newsFeedTeamParticipantRepository)
        {
            _newsFeedTeamParticipantRepository = newsFeedTeamParticipantRepository;
            _newsFeedTeamRepository = newsFeedTeamRepository;
            _newsFeedPostRepository = newsFeedPostRepository;
        }

        public async Task Consume(TeamDeletedEvent message)
        {
            await _newsFeedTeamParticipantRepository.DeleteTeamParticipantsByTeam(message.Id);
            await _newsFeedTeamRepository.DeleteTeam(message.Id);
            await _newsFeedPostRepository.DeletePostsByTeam(message.Id);
        }
    }
}