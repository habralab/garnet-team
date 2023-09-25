using System.Security.Claims;
using Garnet.Common.Application;
using HotChocolate.Execution;

namespace Garnet.Common.Infrastructure.Identity;

public class CurrentUserProvider : ICurrentUserProvider
{
    public CurrentUserProvider(ClaimsPrincipal? claimsPrincipal)
    {
        UserId = claimsPrincipal?.FindFirstValue(ClaimTypes.NameIdentifier)
                 ?? throw new QueryException("У пользователя в токене доступа отсутствует идентификатор");
    }

    public string UserId { get; }
}