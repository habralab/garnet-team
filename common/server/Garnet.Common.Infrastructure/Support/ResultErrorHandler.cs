using Garnet.Common.Application.Errors;
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
                var errors = result.Errors
                    .Select(x => new Error(x.Message,
                        x is ApplicationError garnetError
                            ? garnetError.Code
                            : null));
                throw new QueryException(errors);
            }
        }
    }
}