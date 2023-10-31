using Garnet.Notifications.Infrastructure.Api.NotificationDelete;
using HotChocolate.Types;

namespace Garnet.Notifications.Infrastructure.Api
{
    [ExtendObjectType("Mutation")]
    public class NotificationsMutation
    {
        public Task<NotificationDeletePayload> NotificationDelete(CancellationToken ct, string notificationId)
        {
            return null;
        }
    }
}