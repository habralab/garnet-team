using Garnet.Common.Application.MessageBus;
using Garnet.Common.Infrastructure.Support;
using Garnet.NewsFeed.Application.NewsFeedTeamParticipant;
using Garnet.Teams.Events.TeamJoinInvitation;

namespace Garnet.NewsFeed.Infrastructure.EventHandlers.Team
{
    public class NewsFeedTeamJoinInvitationDecidedEventConsumer : IMessageBusConsumer<TeamJoinInvitationDecidedEvent>
    {
        private readonly INewsFeedTeamParticipantRepository _newsFeedTeamParticipantRepository;

        public NewsFeedTeamJoinInvitationDecidedEventConsumer(INewsFeedTeamParticipantRepository newsFeedTeamParticipantRepository)
        {
            _newsFeedTeamParticipantRepository = newsFeedTeamParticipantRepository;
        }

        public async Task Consume(TeamJoinInvitationDecidedEvent message)
        {
            if (message.IsApproved)
            {
                await _newsFeedTeamParticipantRepository.CreateTeamParticipant(Uuid.NewGuid(), message.TeamId, message.UserId);
            }
        }
    }
}