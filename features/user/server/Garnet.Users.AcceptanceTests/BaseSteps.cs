using Garnet.Common.AcceptanceTests.Support;
using Garnet.Users.Infrastructure.Api;
using Garnet.Users.Infrastructure.MongoDb;

namespace Garnet.Users.AcceptanceTests;

public abstract class BaseSteps
{
    protected Db Db { get; }
    protected UsersQuery Query { get; }
    protected UsersMutation Mutation { get; }
    protected GiveMe GiveMe { get; }

    protected BaseSteps(StepsArgs args)
    {
        Db = args.Db;
        Query = args.Query;
        Mutation = args.Mutation;
        GiveMe = args.GiveMe;
    }
}