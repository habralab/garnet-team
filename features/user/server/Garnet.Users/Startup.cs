using System.Diagnostics.CodeAnalysis;
using Garnet.Common.Application;
using Garnet.Common.Infrastructure.Api;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Common.Infrastructure.MessageBus;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.MongoDb.Migrations;
using Garnet.Common.Infrastructure.S3;
using Garnet.Common.Infrastructure.Support;
using Garnet.Users.Application;
using Garnet.Users.Application.Commands;
using Garnet.Users.Application.Queries;
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
        builder.Services.AddGarnetAuthorization();
        builder.Services.AddGarnetUsersInternal();
        builder.Services.AddGarnetUsersMessageBus(nameof(Users));
        builder.Services.AddGarnetPublicStorage();
        builder.Services.AddRepeatableMigrations();

        return builder;
    }

    private static void AddGarnetUsersInternal(this IServiceCollection services)
    {
        services.AddScoped<DbFactory>(o => new DbFactory(EnvironmentEx.GetRequiredEnvironmentVariable("MONGO_CONNSTRING")));
        services.AddGarnetMongoSerializers();

        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddScoped<IUsersRepository, UsersRepository>();

        services.AddGarnetUsersDomain();
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

    public static void AddGarnetUsersDomain(this IServiceCollection services)
    {
        services.AddScoped<UserEditTagsCommand>();
        services.AddScoped<UserCreateCommand>();
        services.AddScoped<UserEditDescriptionCommand>();
        services.AddScoped<UserUploadAvatarCommand>();
        services.AddScoped<UserEditUsernameCommand>();

        services.AddScoped<UserGetQuery>();
        services.AddScoped<UsersFilterQuery>();
    }
}