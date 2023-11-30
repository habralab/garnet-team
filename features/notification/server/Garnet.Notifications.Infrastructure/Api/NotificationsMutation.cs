using Garnet.Common.Infrastructure.Support;
using Garnet.Notifications.Application.Commands;
using Garnet.Notifications.Infrastructure.Api.NotificationDelete;
using Garnet.Notifications.Infrastructure.Api.NotificationGet;
using HotChocolate.Types;

namespace Garnet.Notifications.Infrastructure.Api
{
    [ExtendObjectType("Mutation")]
    public class NotificationsMutation
    {
        private readonly NotificationDeleteAsReadCommand _notificationDeleteAsReadCommand;

        public NotificationsMutation(NotificationDeleteAsReadCommand notificationDeleteAsReadCommand)
        {
            _notificationDeleteAsReadCommand = notificationDeleteAsReadCommand;
        }

        public async Task<NotificationDeletePayload> NotificationDelete(CancellationToken ct, string notificationId)
        {
            var result = await _notificationDeleteAsReadCommand.Execute(ct, notificationId);
            result.ThrowQueryExceptionIfHasErrors();

            var notification = result.Value;
            return new NotificationDeletePayload(
                notification.Id,
                notification.Title,
                notification.Body,
                notification.Type,
                notification.UserId,
                notification.CreatedAt,
                notification.LinkedEntityId,
                notification.QuotedEntities.Select(y => new QuotedEntityPayload(
                    y.Id,
                    y.AvatarUrl,
                    y.Quote
                )).ToArray()
            );
        }
    }
}