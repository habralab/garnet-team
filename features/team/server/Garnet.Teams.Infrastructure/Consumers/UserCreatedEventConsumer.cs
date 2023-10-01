using Garnet.Common.Application.MessageBus;
using Garnet.Users.Events;

namespace Garnet.Teams.Infrastructure.Consumers
{
    public class UserCreatedEventConsumer : IMessageBusConsumer<UserCreatedEvent>
    {
        public Task Consume(UserCreatedEvent message)
        {
            throw new NotImplementedException();
        }
    }
}