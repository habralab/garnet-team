using HotChocolate.Execution;

namespace Garnet.Common.AcceptanceTests.Contexts
{
    public class QueryExceptionsContext
    {
        public List<QueryException> QueryExceptions {get;set;} = new();
    }
}