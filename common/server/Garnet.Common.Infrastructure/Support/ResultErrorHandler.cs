using HotChocolate.Execution;
using Error = HotChocolate.Error;

namespace Garnet.Common.Infrastructure.Support
{
    public static class ResultErrorHandler
    {
        public static void ThrowQueryExceptionIfHasErrors<T>(this FluentResults.Result<T> result)
        {
            if (result.IsFailed)
            {
                throw new QueryException(result.Errors.Select(x => new Error(x.Message)));
            }
        }
    }
}