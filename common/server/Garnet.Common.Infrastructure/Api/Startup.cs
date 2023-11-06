using Garnet.Common.Infrastructure.Api.Cancellation;
using HotChocolate.Execution.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Garnet.Common.Infrastructure.Api;

public static class Startup
{
    public static IRequestExecutorBuilder AddApiType<TType>(this IRequestExecutorBuilder builder)
        where TType : class
    {
        builder.AddType<TType>();
        builder.Services.AddScoped<TType>();
        return builder;
    }

    public static IServiceCollection AddCancellationTokenProvider(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<CancellationTokenProvider>();
        return services;
    }
}