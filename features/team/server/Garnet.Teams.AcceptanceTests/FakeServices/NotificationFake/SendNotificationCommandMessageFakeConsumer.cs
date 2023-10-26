using Garnet.Common.Application.MessageBus;
using Garnet.Notifications.Events;

namespace Garnet.Teams.AcceptanceTests.FakeServices.NotificationFake
{
    public class SendNotificationCommandMessageFakeConsumer : IMessageBusConsumer<SendNotificationCommandMessage>
    {
        public SendNotificationCommandMessage Notification { get; private set; } = null!;

        public Task Consume(SendNotificationCommandMessage message)
        {
            Notification = message;
            return Task.CompletedTask;
        }
    }
}