using Garnet.Common.Application.MessageBus;
using Garnet.Notifications.Events;

namespace Garnet.Notifications.AcceptanceTests.FakeServices
{
    public class FakeNotificationProducer
    {
        private readonly IMessageBus _messageBus;

        public FakeNotificationProducer(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task SendNotification(SendNotificationCommandMessage message)
        {
            await _messageBus.Publish(message);
        }
    }
}