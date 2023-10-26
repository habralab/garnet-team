using Garnet.Common.AcceptanceTests.Support;
using Garnet.Notifications.Infrastructure.Api;
using Garnet.Notifications.Infrastructure.MongoDB;

namespace Garnet.Notifications.AcceptanceTests
{
    public abstract class BaseSteps
    {
        protected GiveMe GiveMe { get; }
        protected Db Db { get; }
        public NotificationQuery Query { get; }
        public NotificationMutation Mutation { get; }

        public BaseSteps(StepsArgs args)
        {
            GiveMe = args.GiveMe;
            Db = args.Db;
            Query = args.Query;
            Mutation = args.Mutation;
        }
    }
}