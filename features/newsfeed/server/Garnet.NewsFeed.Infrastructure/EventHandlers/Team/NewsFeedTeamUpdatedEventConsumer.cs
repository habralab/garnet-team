using Garnet.Common.Application.MessageBus;
using Garnet.NewsFeed.Application.NewsFeedTeam;
using Garnet.Teams.Events.Team;

namespace Garnet.NewsFeed.Infrastructure.EventHandlers.Team
{
    public class NewsFeedTeamUpdatedEventConsumer : IMessageBusConsumer<TeamUpdatedEvent>
    {
        private readonly INewsFeedTeamRepository _newsFeedTeamRepository;

        public NewsFeedTeamUpdatedEventConsumer(INewsFeedTeamRepository newsFeedTeamRepository)
        {
            _newsFeedTeamRepository = newsFeedTeamRepository;
        }

        public async Task Consume(TeamUpdatedEvent message)
        {
            await _newsFeedTeamRepository.UpdateTeamOwner(message.Id, message.OwnerUserId);
        }
    }
}