using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Application;
using Garnet.Common.Infrastructure.Migrations;
using Garnet.Projects.Application;
using Garnet.Projects.Infrastructure.Api;
using Garnet.Projects.Infrastructure.MongoDb;
using Garnet.Projects.Infrastructure.MongoDb.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using SolidToken.SpecFlow.DependencyInjection;

namespace Garnet.Projects.AcceptanceTests;

public static class Startup
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();

        AddMongoDb(services);

        services.AddScoped<CurrentUserProviderFake>();
        services.AddScoped<ICurrentUserProvider>(o => o.GetRequiredService<CurrentUserProviderFake>());

        services.AddScoped<IProjectsRepository, ProjectsRepository>();
        services.AddScoped<ProjectsService>();

        services.AddScoped<ProjectsMutation>();
        services.AddScoped<ProjectsQuery>();

        services.AddScoped<GiveMe>();
        services.AddScoped<StepsArgs>();

        return services;
    }

    private static void AddMongoDb(IServiceCollection services)
    {
        services.AddScoped<MongoDbRunner>(_ => MongoDbRunner.Start());
        services.AddScoped<DbFactory>(o =>
        {
            var mongo = o.GetRequiredService<MongoDbRunner>();
            return new DbFactory(mongo.ConnectionString);
        });
        services.AddScoped<Db>(o => o.GetRequiredService<DbFactory>().Create());

        services.AddScoped<IRepeatableMigration, CreateIndexesMigration>();
    }
}