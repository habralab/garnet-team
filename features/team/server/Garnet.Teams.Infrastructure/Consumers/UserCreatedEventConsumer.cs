using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application;
using Garnet.Users.Events;

namespace Garnet.Teams.Infrastructure.Consumers
{
    public class UserCreatedEventConsumer : IMessageBusConsumer<UserCreatedEvent>
    {
        private readonly UserService _userService;
        public UserCreatedEventConsumer(UserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(UserCreatedEvent message)
        {
            await _userService.AddUser(CancellationToken.None, message.UserId);
        }
    }
}