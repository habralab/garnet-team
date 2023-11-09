using Garnet.Common.AcceptanceTests.Support;
using Garnet.NewsFeed.Infrastructure.Api;
using Garnet.NewsFeed.Infrastructure.MongoDB;

namespace Garnet.NewsFeed.AcceptanceTests
{
    public class StepsArgs
    {
        public GiveMe GiveMe { get; }
        public Db Db { get; }
        public NewsFeedQuery Query { get; }
        public NewsFeedMutation Mutation { get; }

        public StepsArgs(GiveMe giveMe, Db db, NewsFeedQuery query, NewsFeedMutation mutation)
        {
            GiveMe = giveMe;
            Db = db;
            Query = query;
            Mutation = mutation;
        }
    }
}