using Garnet.Common.Application.MessageBus;
using Garnet.Notifications.Events;

namespace Garnet.Teams.AcceptanceTests.FakeServices.NotificationFake
{
    public class DeleteNotificationCommandMessageFakeConsumer : IMessageBusConsumer<DeleteNotificationCommandMessage>
    {
        public List<DeleteNotificationCommandMessage> DeletedNotifications = new();
        public Task Consume(DeleteNotificationCommandMessage message)
        {
            DeletedNotifications.Add(message);
            return Task.CompletedTask;
        }
    }
}