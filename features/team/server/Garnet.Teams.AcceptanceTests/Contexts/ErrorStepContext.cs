using HotChocolate.Execution;

namespace Garnet.Teams.AcceptanceTests.Contexts
{
    public class ErrorStepContext
    {
        public ErrorStepContext()
        {
            QueryException = new QueryException();
        }

        public QueryException QueryException {get;set;}
    }
}