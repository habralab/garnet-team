using Garnet.Common.AcceptanceTests.Support;
using Garnet.NewsFeed.Infrastructure.Api;
using Garnet.NewsFeed.Infrastructure.MongoDB;

namespace Garnet.NewsFeed.AcceptanceTests
{
    public class BaseSteps
    {
        protected GiveMe GiveMe { get; }
        protected Db Db { get; }
        public NewsFeedQuery Query { get; }
        public NewsFeedMutation Mutation { get; }

        public BaseSteps(StepsArgs args)
        {
            GiveMe = args.GiveMe;
            Db = args.Db;
            Query = args.Query;
            Mutation = args.Mutation;
        }
    }
}