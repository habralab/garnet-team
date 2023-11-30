using System.Threading.Tasks;
using Garnet.Notifications.Application.Queries;
using Garnet.Notifications.Infrastructure.Api.NotificationGet;
using HotChocolate.Types;

namespace Garnet.Notifications.Infrastructure.Api
{
    [ExtendObjectType("Query")]

    public class NotificationsQuery
    {
        private readonly NotificationsGetListByCurrentUserQuery _notificationGetQuery;
        public NotificationsQuery(NotificationsGetListByCurrentUserQuery notificationGetQuery)
        {
            _notificationGetQuery = notificationGetQuery;
        }

        public async Task<NotificationGetPayload> NotificationsGetListByCurrentUser(CancellationToken ct)
        {
            var result = await _notificationGetQuery.Query(ct);

            var notifications = result.Select(x =>
            {
                var quote = x.QuotedEntities.Select(y => new QuotedEntityPayload(
                    y.Id,
                    y.AvatarUrl,
                    y.Quote
                ));

                return new NotificationPayload(
                x.Id,
                x.Title,
                x.Body,
                x.Type,
                x.UserId,
                x.CreatedAt,
                x.LinkedEntityId,
                quote.ToArray());
            });
            return new NotificationGetPayload(notifications.ToArray());
        }
    }

}