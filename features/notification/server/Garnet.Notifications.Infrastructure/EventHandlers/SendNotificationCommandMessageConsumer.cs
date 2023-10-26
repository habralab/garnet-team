using Garnet.Common.Application.MessageBus;
using Garnet.Notifications.Application;
using Garnet.Notifications.Application.Args;
using Garnet.Notifications.Events;

namespace Garnet.Notifications.Infrastructure.EventHandlers
{
    public class SendNotificationCommandMessageConsumer : IMessageBusConsumer<SendNotificationCommandMessage>
    {
        private readonly INotificationRepository _notificationRepository;

        public SendNotificationCommandMessageConsumer(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task Consume(SendNotificationCommandMessage message)
        {
            var args = new NotificationCreateArgs(
                message.Title,
                message.Body,
                message.Type,
                message.UserId,
                message.CreatedAt,
                message.LinkedEntityId
            );

            await _notificationRepository.CreateNotification(CancellationToken.None, args);
        }
    }
}