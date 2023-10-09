using Garnet.Common.Application.MessageBus;
using Garnet.Teams.Application.TeamUser.Commands;
using Garnet.Users.Events;

namespace Garnet.Teams.Infrastructure.EventHandlers.User
{
    public class UserCreatedEventConsumer : IMessageBusConsumer<UserCreatedEvent>
    {
        private readonly TeamUserCreateCommand _teamUserCreateCommand;
        public UserCreatedEventConsumer(TeamUserCreateCommand teamUserCreateCommand)
        {
            _teamUserCreateCommand = teamUserCreateCommand;
        }

        public async Task Consume(UserCreatedEvent message)
        {
            await _teamUserCreateCommand.Execute(CancellationToken.None, message.UserId, message.UserName);
        }
    }
}