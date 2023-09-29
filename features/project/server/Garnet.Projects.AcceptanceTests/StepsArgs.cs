using Garnet.Common.AcceptanceTests.Support;
using Garnet.Projects.Infrastructure.Api;
using Garnet.Projects.Infrastructure.MongoDb;

namespace Garnet.Projects.AcceptanceTests;

public class StepsArgs
{
    public Db Db { get; }
    public ProjectsMutation Mutation { get; }
    public GiveMe GiveMe { get; }

    public StepsArgs(Db db, ProjectsMutation mutation, GiveMe giveMe)
    {
        Db = db;
        Mutation = mutation;
        GiveMe = giveMe;
    }
}