using Garnet.Common.Application.MessageBus;
using Garnet.Notifications.Application.Args;
using Garnet.Notifications.Application.Commands;
using Garnet.Notifications.Events;

namespace Garnet.Notifications.Infrastructure.EventHandlers
{
    public class DeleteNotificationCommandMessageConsumer : IMessageBusConsumer<DeleteNotificationCommandMessage>
    {
        private readonly NotificationDeleteCommand _notificationDeleteCommand;

        public DeleteNotificationCommandMessageConsumer(NotificationDeleteCommand notificationDeleteCommand)
        {
            _notificationDeleteCommand = notificationDeleteCommand;
        }

        public async Task Consume(DeleteNotificationCommandMessage message)
        {
            var args = new NotificationDeleteArgs(
                message.UserId,
                message.Type,
                message.LinkedEntityId
            );

            await _notificationDeleteCommand.Execute(CancellationToken.None, args);
        }
    }
}