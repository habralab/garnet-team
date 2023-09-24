using Garnet.Common.AcceptanceTests.Support;
using Garnet.Users.Infrastructure.Api;
using Garnet.Users.Infrastructure.MongoDb;

namespace Garnet.Users.AcceptanceTests;

public class StepsArgs
{
    public Db Db { get; }
    public UsersQuery Query { get; }
    public UsersMutation Mutation { get; }
    public GiveMe GiveMe { get; }

    public StepsArgs(Db db, UsersQuery query, UsersMutation mutation, GiveMe giveMe)
    {
        Db = db;
        Query = query;
        Mutation = mutation;
        GiveMe = giveMe;
    }
}