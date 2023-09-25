using System.Security.Claims;

namespace Garnet.Common.Application;

public interface ICurrentUserProvider
{
    string UserId { get; }
}