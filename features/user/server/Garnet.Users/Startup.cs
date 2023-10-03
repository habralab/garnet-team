using System.Diagnostics.CodeAnalysis;
using Garnet.Common.Infrastructure.MessageBus;
using Garnet.Common.Infrastructure.Migrations;
using Garnet.Common.Infrastructure.S3;
using Garnet.Users.Application;
using Garnet.Users.Events;
using Garnet.Users.Infrastructure.Api;
using Garnet.Users.Infrastructure.MongoDb;
using Garnet.Users.Infrastructure.MongoDb.Migrations;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Garnet.User;

[ExcludeFromCodeCoverage]
public static class Startup
{
    public static IRequestExecutorBuilder AddGarnetUsers(this IRequestExecutorBuilder builder)
    {
        builder.AddType<UsersQuery>();
        builder.AddType<UsersMutation>();
        builder.Services.AddGarnetUsersInternal();
        builder.Services.AddGarnetUsersMessageBus(nameof(Users));
        builder.Services.AddRepeatableMigrations();
        builder.Services.AddGarnetPublicStorage();

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

    public static void AddGarnetUsersMessageBus(this IServiceCollection services, string name)
    {
        services.AddGarnetMessageBus(name, o =>
        {
            o.RegisterMessage<UserCreatedEvent>();
            o.RegisterMessage<UserUpdatedEvent>();
        });
    }

    public static void AddRepeatableMigrations(this IServiceCollection services)
    {
        services.AddScoped<IRepeatableMigration, CreateIndexesMigration>();
    }
}