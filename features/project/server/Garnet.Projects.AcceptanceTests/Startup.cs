using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Application;
using Garnet.Common.Application.S3;
using Garnet.Common.Infrastructure.Support;
using Garnet.Project;
using Garnet.Projects.Infrastructure.Api;
using Garnet.Projects.Infrastructure.MongoDb;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using Garnet.Common.Infrastructure.MessageBus;
using SolidToken.SpecFlow.DependencyInjection;
using Garnet.Project.AcceptanceTests.FakeServices.NotificationFake;
using Garnet.Notifications.Events;

namespace Garnet.Projects.AcceptanceTests;

public static class Startup
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();

        AddMongoDb(services);
        AddMessageBus(services);

        services.AddProjectsInternal();
        services.AddProjectUsersInternal();
        services.AddProjectTeamsInternal();
        services.AddProjectTeamParticipantsInternal();
        services.AddProjectTeamJoinRequestsInternal();
        services.AddProjectTasksInternal();

        services.AddScoped<CurrentUserProviderFake>();
        services.AddScoped<ICurrentUserProvider>(o => o.GetRequiredService<CurrentUserProviderFake>());

        services.AddScoped<RemoteFileStorageFake>();
        services.AddScoped<IRemoteFileStorage>(o => o.GetRequiredService<RemoteFileStorageFake>());

        services.AddScoped<DateTimeServiceFake>();
        services.AddScoped<IDateTimeService>(o => o.GetRequiredService<DateTimeServiceFake>());

        services.AddScoped<ProjectsMutation>();
        services.AddScoped<ProjectsQuery>();

        services.AddScoped<QueryExceptionsContext>();

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

        services.AddRepeatableMigrations();
    }

    private static void AddMessageBus(IServiceCollection services)
    {
        services.AddGarnetProjectsMessageBus(Uuid.NewGuid());
        services.AddGarnetMessageBus(Uuid.NewGuid(), o =>
        {
            o.RegisterConsumer<SendNotificationCommandMessageFakeConsumer, SendNotificationCommandMessage>();
        });
    }
}