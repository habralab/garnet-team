using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.TeamUser;
using Garnet.Teams.Application.TeamUser.Args;
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
            var args = new TeamUserCreateArgs(message.UserId, message.UserName);
            await _teamUserRepository.AddUser(CancellationToken.None, args);
        }
    }
}