using Garnet.Common.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Garnet.Common.Infrastructure.Identity;

public static class Startup
{
    public static IServiceCollection AddCurrentUserProvider(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserProvider>(o =>
        {
            var currentUserClaims =
                o.GetService<IHttpContextAccessor>()?.HttpContext?.User
                ?? ClaimsFactory.System();
            return new CurrentUserProvider(currentUserClaims);
        });
        return services;
    }
}