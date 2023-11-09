using Garnet.Common.Application.MessageBus;
using Garnet.Common.Infrastructure.Support;
using Garnet.NewsFeed.Application.NewsFeedTeamParticipant;
using Garnet.Teams.Events.TeamUserJoinRequest;

namespace Garnet.NewsFeed.Infrastructure.EventHandlers.Team
{
    public class NewsFeedTeamUserJoinRequestDecidedEventConsumer : IMessageBusConsumer<TeamUserJoinRequestDecidedEvent>
    {
        private readonly INewsFeedTeamParticipantRepository _newsFeedTeamParticipantRepository;

        public NewsFeedTeamUserJoinRequestDecidedEventConsumer(INewsFeedTeamParticipantRepository newsFeedTeamParticipantRepository)
        {
            _newsFeedTeamParticipantRepository = newsFeedTeamParticipantRepository;
        }

        public async Task Consume(TeamUserJoinRequestDecidedEvent message)
        {
            if (message.IsApproved)
            {
                await _newsFeedTeamParticipantRepository.CreateTeamParticipant(Uuid.NewGuid(), message.TeamId, message.UserId);
            }
        }
    }
}