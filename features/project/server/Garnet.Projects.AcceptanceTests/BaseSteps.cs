using Garnet.Common.AcceptanceTests.Support;
using Garnet.Projects.Infrastructure.Api;
using Garnet.Projects.Infrastructure.MongoDb;

namespace Garnet.Projects.AcceptanceTests;

public abstract class BaseSteps
{
    protected Db Db { get; }
    protected ProjectsQuery Query { get; }
    protected ProjectsMutation Mutation { get; }
    protected GiveMe GiveMe { get; }

    protected BaseSteps(StepsArgs args)
    {
        Db = args.Db;
        Query = args.Query;
        Mutation = args.Mutation;
        GiveMe = args.GiveMe;
    }
}