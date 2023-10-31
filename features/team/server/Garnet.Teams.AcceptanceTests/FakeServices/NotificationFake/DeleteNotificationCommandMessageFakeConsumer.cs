using Garnet.Common.Application.MessageBus;
using Garnet.Notifications.Events;

namespace Garnet.Teams.AcceptanceTests.FakeServices.NotificationFake
{
    public class DeleteNotificationCommandMessageFakeConsumer : IMessageBusConsumer<DeleteNotificationCommandMessage>
    {
        public List<DeleteNotificationCommandMessage> Notifications = new();
        public Task Consume(DeleteNotificationCommandMessage message)
        {
            Notifications.Add(message);
            return Task.CompletedTask;
        }
    }
}