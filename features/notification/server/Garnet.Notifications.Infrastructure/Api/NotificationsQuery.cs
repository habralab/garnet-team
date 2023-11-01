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

            var notifications = result.Select(x => new NotificationPayload(
                x.Id,
                x.Title,
                x.Body,
                x.Type,
                x.UserId,
                x.CreatedAt,
                x.LinkedEntityId));
            return new NotificationGetPayload(notifications.ToArray());
        }
    }

}