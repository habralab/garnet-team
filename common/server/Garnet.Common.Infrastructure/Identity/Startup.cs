using Garnet.Common.Application;
using Garnet.Common.Infrastructure.Identity.Kratos;
using Garnet.Common.Infrastructure.Identity.SecretKey;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Garnet.Common.Infrastructure.Identity;

public static class Startup
{
    public static IServiceCollection AddGarnetAuthorization(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserProvider>(o =>
        {
            var currentUserClaims =
                o.GetService<IHttpContextAccessor>()?.HttpContext?.User
                ?? ClaimsFactory.System(AuthSchemas.SecretKey);
            return new CurrentUserProvider(currentUserClaims);
        });
        return services;
    }
    
    public static IServiceCollection AddSecretKeyAuth(this IServiceCollection services)
    {
        services.AddScoped<KratosProvider>();
        services
            .AddAuthentication(AuthSchemas.SecretKey)
            .AddScheme<SecretKeyAuthenticationSchemeOptions, SecretKeyAuthenticationHandler>(AuthSchemas.SecretKey, null);
        return services;
    }
    
    public static IServiceCollection AddKratosAuth(this IServiceCollection services)
    {
        services
            .AddAuthentication(AuthSchemas.Kratos)
            .AddScheme<KratosAuthenticationSchemeOptions, KratosAuthenticationHandler>(AuthSchemas.Kratos, null);
        return services;
    }
}