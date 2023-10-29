using Garnet.Common.Application.MessageBus;
using Garnet.Notifications.Events;

namespace Garnet.Project.AcceptanceTests.FakeServices.NotificationFake
{
    public class SendNotificationCommandMessageFakeConsumer : IMessageBusConsumer<SendNotificationCommandMessage>
    {
        public List<SendNotificationCommandMessage> Notifications = new();

        public Task Consume(SendNotificationCommandMessage message)
        {
            Notifications.Add(message);
            return Task.CompletedTask;
        }
    }
}