using Garnet.Common.Infrastructure.Migrations;
using Garnet.Projects.Application;
using Garnet.Projects.Infrastructure.Api;
using Garnet.Projects.Infrastructure.MongoDb;
using Garnet.Projects.Infrastructure.MongoDb.Migrations;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Garnet.Project;

public static class Startup
{
    public static IRequestExecutorBuilder AddGarnetProjects(this IRequestExecutorBuilder builder)
    {
        builder.AddType<ProjectsMutation>();
        builder.Services.AddGarnetProjectsInternal();
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

    private static void AddRepeatableMigrations(this IServiceCollection services)
    {
        services.AddScoped<IRepeatableMigration, CreateIndexesMigration>();
    }
}