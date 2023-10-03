using System.Diagnostics.CodeAnalysis;
using Garnet.Common.Infrastructure.MessageBus;
using Garnet.Common.Infrastructure.Migrations;
using Garnet.Projects.Application;
using Garnet.Projects.Events;
using Garnet.Projects.Infrastructure.Api;
using Garnet.Projects.Infrastructure.MongoDb;
using Garnet.Projects.Infrastructure.MongoDb.Migrations;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Garnet.Project;

[ExcludeFromCodeCoverage]
public static class Startup
{
    public static IRequestExecutorBuilder AddGarnetProjects(this IRequestExecutorBuilder builder)
    {
        builder.AddType<ProjectsMutation>();
        builder.AddType<ProjectsQuery>();
        builder.Services.AddGarnetProjectsInternal();
        builder.Services.AddGarnetProjectsMessageBus(nameof(Projects));
        builder.Services.AddRepeatableMigrations();


        return builder;
    }

    private static void AddGarnetProjectsInternal(this IServiceCollection services)
    {
        const string mongoConnStringEnv = "MONGO_CONNSTRING";
        var mongoDbConnString =
            Environment.GetEnvironmentVariable(mongoConnStringEnv)
            ?? throw new Exception($"No {mongoConnStringEnv} environment variable was provided.");
        services.AddScoped<DbFactory>(o => new DbFactory(mongoDbConnString));
        services.AddScoped<ProjectsService>();
        services.AddScoped<IProjectsRepository, ProjectsRepository>();
    }

    public static void AddGarnetProjectsMessageBus(this IServiceCollection services, string name)
    {
        services.AddGarnetMessageBus(name, o =>
        {
            o.RegisterMessage<ProjectCreatedEvent>();
            o.RegisterMessage<ProjectUpdatedEvent>();
            o.RegisterMessage<ProjectDeletedEvent>();
        });
    }

    public static void AddRepeatableMigrations(this IServiceCollection services)
    {
        services.AddScoped<IRepeatableMigration, CreateIndexesMigration>();
    }
}