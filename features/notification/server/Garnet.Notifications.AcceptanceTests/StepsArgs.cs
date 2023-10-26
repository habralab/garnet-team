using Garnet.Common.AcceptanceTests.Support;
using Garnet.Notifications.Infrastructure.Api;
using Garnet.Notifications.Infrastructure.MongoDB;

namespace Garnet.Notifications.AcceptanceTests
{
    public class StepsArgs
    {
        public GiveMe GiveMe { get; }
        public Db Db { get; }
        public NotificationQuery Query { get; }
        public NotificationMutation Mutation { get; }

        public StepsArgs(GiveMe giveMe, Db db, NotificationQuery query, NotificationMutation mutation)
        {
            GiveMe = giveMe;
            Db = db;
            Query = query;
            Mutation = mutation;
        }
    }
}