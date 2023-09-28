using Garnet.Common.Infrastructure.Migrations;
using Garnet.Users.Application;
using Garnet.Users.Infrastructure.Api;
using Garnet.Users.Infrastructure.MongoDb;
using Garnet.Users.Infrastructure.MongoDb.Migrations;
using HotChocolate.Execution.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Garnet.User;

public static class Startup
{
    public static IRequestExecutorBuilder AddGarnetUsers(this IRequestExecutorBuilder builder)
    {
        builder.AddType<UsersQuery>();
        builder.AddType<UsersMutation>();
        builder.Services.AddGarnetUsersInternal();
        builder.Services.AddRepeatableMigrations();

        return builder;
    }
    
    private static void AddGarnetUsersInternal(this IServiceCollection services)
    {
        const string mongoConnStringEnv = "MONGO_CONNSTRING";
        var mongoDbConnString =
            Environment.GetEnvironmentVariable(mongoConnStringEnv)
            ?? throw new Exception($"No {mongoConnStringEnv} environment variable was provided.");
        services.AddScoped<DbFactory>(o => new DbFactory(mongoDbConnString));
        services.AddScoped<UsersService>();
        services.AddScoped<IUsersRepository, UsersRepository>();
    }

    private static void AddRepeatableMigrations(this IServiceCollection services)
    {
        services.AddScoped<IRepeatableMigration, CreateIndexesMigration>();
    }
}