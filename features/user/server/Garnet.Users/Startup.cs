using System.Diagnostics.CodeAnalysis;
using Garnet.Common.Application;
using Garnet.Common.Infrastructure.Api;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Common.Infrastructure.MessageBus;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.MongoDb.Migrations;
using Garnet.Common.Infrastructure.S3;
using Garnet.Users.Application;
using Garnet.Users.Application.Commands;
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
        builder.AddApiType<UsersQuery>();
        builder.AddApiType<UsersMutation>();
        builder.Services.AddCurrentUserProvider();
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
        services.AddGarnetMongoSerializers();
        
        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddScoped<UsersService>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<UserCreateCommand>();
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