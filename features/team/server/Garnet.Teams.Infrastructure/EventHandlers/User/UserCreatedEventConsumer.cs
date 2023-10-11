using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.TeamUser;
using Garnet.Users.Events;

namespace Garnet.Teams.Infrastructure.EventHandlers.User
{
    public class UserCreatedEventConsumer : IMessageBusConsumer<UserCreatedEvent>
    {
        private readonly ITeamUserRepository _teamUserRepository;
        public UserCreatedEventConsumer(ITeamUserRepository teamUserRepository)
        {
            _teamUserRepository = teamUserRepository;
        }

        public async Task Consume(UserCreatedEvent message)
        {
            await _teamUserRepository.AddUser(CancellationToken.None, message.UserId, message.UserName);
        }
    }
}