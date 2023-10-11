using System.Diagnostics.CodeAnalysis;
using Garnet.Common.Infrastructure.MessageBus;
using Garnet.Common.Infrastructure.Migrations;
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
using Garnet.Projects.Events.Project;
using Garnet.Projects.Events.ProjectTeamJoinRequest;
using Garnet.Projects.Infrastructure.Api;
using Garnet.Projects.Infrastructure.EventHandlers.ProjectTeamJoinRequest;
using Garnet.Projects.Infrastructure.EventHandlers.Team;
using Garnet.Projects.Infrastructure.EventHandlers.User;
using Garnet.Projects.Infrastructure.MongoDb;
using Garnet.Projects.Infrastructure.MongoDb.Migrations;
using Garnet.Projects.Infrastructure.MongoDb.Project;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeam;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeamJoinRequest;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeamParticipant;
using Garnet.Projects.Infrastructure.MongoDb.ProjectUser;
using Garnet.Teams.Events;
using Garnet.Users.Events;
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

        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectUserRepository, ProjectUserRepository>();
        services.AddScoped<IProjectTeamRepository, ProjectTeamRepository>();
        services.AddScoped<IProjectTeamParticipantRepository, ProjectTeamParticipantRepository>();
        services.AddScoped<IProjectTeamJoinRequestRepository, ProjectTeamJoinRequestRepository>();

        services.AddScoped<ProjectCreateCommand>();
        services.AddScoped<ProjectDeleteCommand>();
        services.AddScoped<ProjectEditDescriptionCommand>();
        services.AddScoped<ProjectEditOwnerCommand>();
        services.AddScoped<ProjectTeamCreateCommand>();
        services.AddScoped<ProjectTeamUpdateCommand>();
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
    }

    public static void AddGarnetProjectsMessageBus(this IServiceCollection services, string name)
    {
        services.AddGarnetMessageBus(name, o =>
        {
            o.RegisterMessage<ProjectCreatedEvent>();
            o.RegisterMessage<ProjectUpdatedEvent>();
            o.RegisterMessage<ProjectDeletedEvent>();
            o.RegisterMessage<ProjectTeamJoinRequestDecidedEvent>();

            o.RegisterConsumer<UserCreatedEventConsumer, UserCreatedEvent>();
            o.RegisterConsumer<TeamCreatedEventConsumer, TeamCreatedEvent>();
            o.RegisterConsumer<TeamUpdatedEventConsumer, TeamUpdatedEvent>();
            o.RegisterConsumer<ProjectTeamJoinRequestCreatedConsumer, TeamJoinProjectRequestCreatedEvent>();
        });
    }

    public static void AddRepeatableMigrations(this IServiceCollection services)
    {
        services.AddScoped<IRepeatableMigration, CreateIndexesMigration>();
    }
}