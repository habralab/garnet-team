using Garnet.Common.AcceptanceTests.Support;
using Garnet.Teams.Infrastructure.Api;
using Garnet.Teams.Infrastructure.MongoDb;

namespace Garnet.Teams.AcceptanceTests
{
    public class StepsArgs
    {
        public GiveMe GiveMe { get; }
        public Db Db { get; }
        public TeamsMutation Mutation { get; }
        public TeamsQuery Query { get; }

        public StepsArgs(GiveMe giveMe, Db db, TeamsMutation mutation, TeamsQuery query)
        {
            GiveMe = giveMe;
            Db = db;
            Mutation = mutation;
            Query = query;
        }
    }
}