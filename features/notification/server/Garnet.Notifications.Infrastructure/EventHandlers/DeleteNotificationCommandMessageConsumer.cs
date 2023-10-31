using Garnet.Common.Application.MessageBus;
using Garnet.Notifications.Events;

namespace Garnet.Notifications.Infrastructure.EventHandlers
{
    public class DeleteNotificationCommandMessageConsumer : IMessageBusConsumer<DeleteNotificationCommandMessage>
    {
        public Task Consume(DeleteNotificationCommandMessage message)
        {
            throw new NotImplementedException();
        }
    }
}