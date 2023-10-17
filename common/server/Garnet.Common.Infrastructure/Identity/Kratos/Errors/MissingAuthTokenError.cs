using Garnet.Common.Application.Errors;

namespace Garnet.Common.Infrastructure.Identity.Kratos.Errors;

public class MissingAuthTokenError : ApplicationError
{
    public MissingAuthTokenError(string message) : base(message)
    {
    }

    public override string Code => nameof(MissingAuthTokenError);
}