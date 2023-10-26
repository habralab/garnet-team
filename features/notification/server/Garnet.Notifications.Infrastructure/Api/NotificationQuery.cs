using System.Threading.Tasks;
using Garnet.Notifications.Infrastructure.Api.NotificationGet;
using HotChocolate.Types;

namespace Garnet.Notifications.Infrastructure.Api
{
    [ExtendObjectType("Query")]

    public class NotificationQuery
    {
        public NotificationQuery()
        {

        }

        public Task<NotificationGetPayload> NotificationGet(CancellationToken ct)
        {
            return null;
        }
    }

}