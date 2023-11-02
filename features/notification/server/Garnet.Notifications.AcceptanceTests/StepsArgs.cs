using Garnet.Common.AcceptanceTests.Support;
using Garnet.Notifications.Infrastructure.Api;
using Garnet.Notifications.Infrastructure.MongoDB;

namespace Garnet.Notifications.AcceptanceTests
{
    public class StepsArgs
    {
        public GiveMe GiveMe { get; }
        public Db Db { get; }
        public NotificationsQuery Query { get; }
        public NotificationsMutation Mutation { get; }

        public StepsArgs(GiveMe giveMe, Db db, NotificationsQuery query, NotificationsMutation mutation)
        {
            GiveMe = giveMe;
            Db = db;
            Query = query;
            Mutation = mutation;
        }
    }
}