using Garnet.Common.Application.MessageBus;
using Garnet.Notifications.Events;

namespace Garnet.Notifications.Infrastructure.EventHandlers
{
    public class SendNotificationCommandMessageConsumer : IMessageBusConsumer<SendNotificationCommandMessage>
    {
        public Task Consume(SendNotificationCommandMessage message)
        {
            throw new NotImplementedException();
        }
    }
}