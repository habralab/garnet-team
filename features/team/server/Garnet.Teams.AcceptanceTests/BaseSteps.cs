using Garnet.Common.AcceptanceTests.Support;
using Garnet.Teams.Infrastructure.Api;
using Garnet.Teams.Infrastructure.MongoDb;

namespace Garnet.Teams.AcceptanceTests
{
    public abstract class BaseSteps
    {
        protected GiveMe GiveMe { get; }
        protected Db Db { get; }
        protected TeamsMutation Mutation { get; }
        protected TeamsQuery Query { get; }

        public BaseSteps(StepsArgs args)
        {
            GiveMe = args.GiveMe;
            Db = args.Db;
            Mutation = args.Mutation;
            Query = args.Query;
        }
    }
}
