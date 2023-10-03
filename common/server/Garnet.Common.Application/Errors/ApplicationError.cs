using FluentResults;

namespace Garnet.Common.Application.Errors;

public abstract class ApplicationError : Error
{
    protected ApplicationError(string message) : base(message)
    {
    }
    public abstract string Code { get; }
}