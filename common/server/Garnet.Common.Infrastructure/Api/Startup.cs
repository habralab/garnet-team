using HotChocolate.Execution.Configuration;
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
}