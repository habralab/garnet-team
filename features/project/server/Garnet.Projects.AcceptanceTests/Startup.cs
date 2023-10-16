using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Application;
using Garnet.Common.Application.S3;
using Garnet.Common.Infrastructure.Support;
using Garnet.Project;
using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.Project.Commands;
using Garnet.Projects.Application.Project.Queries;
using Garnet.Projects.Application.ProjectTeam;
using Garnet.Projects.Application.ProjectTeam.Commands;
using Garnet.Projects.Application.ProjectTeam.Queries;
using Garnet.Projects.Application.ProjectTeamJoinRequest;
using Garnet.Projects.Application.ProjectTeamJoinRequest.Commands;
using Garnet.Projects.Application.ProjectTeamJoinRequest.Queries;
using Garnet.Projects.Application.ProjectTeamParticipant;
using Garnet.Projects.Application.ProjectTeamParticipant.Commands;
using Garnet.Projects.Application.ProjectTeamParticipant.Queries;
using Garnet.Projects.Application.ProjectUser;
using Garnet.Projects.Application.ProjectUser.Commands;
using Garnet.Projects.Infrastructure.Api;
using Garnet.Projects.Infrastructure.MongoDb;
using Garnet.Projects.Infrastructure.MongoDb.Project;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeam;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeamJoinRequest;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeamParticipant;
using Garnet.Projects.Infrastructure.MongoDb.ProjectUser;
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
        AddMessageBus(services);

        services.AddScoped<CurrentUserProviderFake>();
        services.AddScoped<ICurrentUserProvider>(o => o.GetRequiredService<CurrentUserProviderFake>());

        services.AddScoped<RemoteFileStorageFake>();
        services.AddScoped<IRemoteFileStorage>(o => o.GetRequiredService<RemoteFileStorageFake>());

        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectUserRepository, ProjectUserRepository>();
        services.AddScoped<IProjectTeamParticipantRepository, ProjectTeamParticipantRepository>();
        services.AddScoped<IProjectTeamJoinRequestRepository, ProjectTeamJoinRequestRepository>();
        services.AddScoped<IProjectTeamRepository, ProjectTeamRepository>();

        services.AddScoped<ProjectCreateCommand>();
        services.AddScoped<ProjectDeleteCommand>();
        services.AddScoped<ProjectEditDescriptionCommand>();
        services.AddScoped<ProjectEditOwnerCommand>();
        services.AddScoped<ProjectEditNameCommand>();
        services.AddScoped<ProjectUploadAvatarCommand>();

        services.AddScoped<ProjectTeamCreateCommand>();

        services.AddScoped<ProjectUserCreateCommand>();

        services.AddScoped<ProjectTeamParticipantCreateCommand>();
        services.AddScoped<ProjectTeamParticipantUpdateCommand>();

        services.AddScoped<ProjectTeamJoinRequestCreateCommand>();
        services.AddScoped<ProjectTeamJoinRequestDecideCommand>();
        services.AddScoped<ProjectTeamJoinRequestUpdateCommand>();

        services.AddScoped<ProjectGetQuery>();
        services.AddScoped<ProjectsFilterQuery>();

        services.AddScoped<ProjectTeamParticipantFilterQuery>();

        services.AddScoped<ProjectTeamJoinRequestFilterQuery>();

        services.AddScoped<ProjectTeamGetQuery>();

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
    }
}