using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application;
using Garnet.Users.Events;

namespace Garnet.Teams.Infrastructure.EventHandlers
{
    public class UserCreatedEventConsumer : IMessageBusConsumer<UserCreatedEvent>
    {
        private readonly TeamUserService _userService;
        public UserCreatedEventConsumer(TeamUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(UserCreatedEvent message)
        {
            await _userService.AddUser(CancellationToken.None, message.UserId);
        }
    }
}