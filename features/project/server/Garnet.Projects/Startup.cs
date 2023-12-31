using System.Diagnostics.CodeAnalysis;
using Garnet.Common.Application;
using Garnet.Common.Infrastructure.Api;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Common.Infrastructure.MessageBus;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.MongoDb.Migrations;
using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.Project.Commands;
using Garnet.Projects.Application.Project.Queries;
using Garnet.Projects.Application.ProjectTask;
using Garnet.Projects.Application.ProjectTask.Commands;
using Garnet.Projects.Application.ProjectTask.Queries;
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
using Garnet.Projects.Application.ProjectUser.Queries;
using Garnet.Projects.Events.Project;
using Garnet.Projects.Events.ProjectTask;
using Garnet.Projects.Events.ProjectTeamJoinRequest;
using Garnet.Projects.Infrastructure.Api;
using Garnet.Projects.Infrastructure.EventHandlers.ProjectTeamJoinRequest;
using Garnet.Projects.Infrastructure.EventHandlers.Team;
using Garnet.Projects.Infrastructure.EventHandlers.User;
using Garnet.Projects.Infrastructure.MongoDb;
using Garnet.Projects.Infrastructure.MongoDb.Migrations;
using Garnet.Projects.Infrastructure.MongoDb.Project;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTask;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeam;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeamJoinRequest;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTeamParticipant;
using Garnet.Projects.Infrastructure.MongoDb.ProjectUser;
using Garnet.Teams.Events.Team;
using Garnet.Teams.Events.TeamJoinInvitation;
using Garnet.Teams.Events.TeamJoinProjectRequest;
using Garnet.Teams.Events.TeamParticipant;
using Garnet.Teams.Events.TeamUserJoinRequest;
using Garnet.Users.Events;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Garnet.Project;

[ExcludeFromCodeCoverage]
public static class Startup
{
    public static IRequestExecutorBuilder AddGarnetProjects(this IRequestExecutorBuilder builder)
    {
        builder.AddApiType<ProjectsMutation>();
        builder.AddApiType<ProjectsQuery>();
        builder.Services.AddGarnetAuthorization();
        builder.Services.AddGarnetProjectsInternal();
        builder.Services.AddGarnetProjectsMessageBus(nameof(Projects));
        builder.Services.AddRepeatableMigrations();

        return builder;
    }

    private static void AddGarnetProjectsInternal(this IServiceCollection services)
    {
        services.AddScoped<DbFactory>(o =>
            new DbFactory(EnvironmentEx.GetRequiredEnvironmentVariable("MONGO_CONNSTRING")));
        services.AddGarnetMongoSerializers();

        services.AddScoped<IDateTimeService, DateTimeService>();

        services.AddProjectsInternal();
        services.AddProjectUsersInternal();
        services.AddProjectTeamsInternal();
        services.AddProjectTeamParticipantsInternal();
        services.AddProjectTeamJoinRequestsInternal();
        services.AddProjectTasksInternal();
    }

    public static void AddGarnetProjectsMessageBus(this IServiceCollection services, string name)
    {
        services.AddGarnetMessageBus(name, o =>
        {
            o.RegisterMessage<ProjectCreatedEvent>();
            o.RegisterMessage<ProjectUpdatedEvent>();
            o.RegisterMessage<ProjectDeletedEvent>();

            o.RegisterMessage<ProjectTaskCreatedEvent>();
            o.RegisterMessage<ProjectTaskUpdatedEvent>();
            o.RegisterMessage<ProjectTaskDeletedEvent>();
            o.RegisterMessage<ProjectTaskClosedEvent>();

            o.RegisterMessage<ProjectTeamJoinRequestDecidedEvent>();

            o.RegisterConsumer<UserCreatedEventConsumer, UserCreatedEvent>();
            o.RegisterConsumer<UserUpdatedEventConsumer, UserUpdatedEvent>();

            o.RegisterConsumer<TeamCreatedEventConsumer, TeamCreatedEvent>();
            o.RegisterConsumer<TeamUpdatedEventConsumer, TeamUpdatedEvent>();

            o.RegisterConsumer<ProjectTeamJoinRequestCreatedConsumer, TeamJoinProjectRequestCreatedEvent>();
            o.RegisterConsumer<TeamUserJoinRequestDecidedEventConsumer, TeamUserJoinRequestDecidedEvent>();
            o.RegisterConsumer<TeamJoinInvitationDecidedEventConsumer, TeamJoinInvitationDecidedEvent>();
            o.RegisterConsumer<ProjectTeamLeaveProjectConsumer, TeamLeaveProjectEvent>();
            o.RegisterConsumer<TeamParticipantLeftTeamEventConsumer, TeamParticipantLeftTeamEvent>();
        });
    }

    public static void AddRepeatableMigrations(this IServiceCollection services)
    {
        services.AddScoped<IRepeatableMigration, CreateIndexesProjectMigration>();
        services.AddScoped<IRepeatableMigration, CreateIndexesProjectTeamParticipantMigration>();
        services.AddScoped<IRepeatableMigration, CreateIndexesProjectTaskMigration>();
    }

    public static void AddProjectsInternal(this IServiceCollection services)
    {
        services.AddScoped<IProjectRepository, ProjectRepository>();

        services.AddScoped<ProjectCreateCommand>();
        services.AddScoped<ProjectDeleteCommand>();
        services.AddScoped<ProjectEditDescriptionCommand>();
        services.AddScoped<ProjectEditOwnerCommand>();
        services.AddScoped<ProjectEditNameCommand>();
        services.AddScoped<ProjectEditTagsCommand>();
        services.AddScoped<ProjectUploadAvatarCommand>();

        services.AddScoped<ProjectGetQuery>();
        services.AddScoped<ProjectsFilterQuery>();
    }

    public static void AddProjectUsersInternal(this IServiceCollection services)
    {
        services.AddScoped<IProjectUserRepository, ProjectUserRepository>();

        services.AddScoped<ProjectUserCreateCommand>();
        services.AddScoped<ProjectUserUpdateCommand>();
    }

    public static void AddProjectTeamsInternal(this IServiceCollection services)
    {
        services.AddScoped<IProjectTeamRepository, ProjectTeamRepository>();

        services.AddScoped<ProjectTeamCreateCommand>();
        services.AddScoped<ProjectTeamUpdateCommand>();
        services.AddScoped<ProjectTeamAddUserParticipantCommand>();
        services.AddScoped<ProjectTeamDeleteUserParticipantCommand>();

        services.AddScoped<ProjectTeamGetQuery>();
    }

    public static void AddProjectTeamParticipantsInternal(this IServiceCollection services)
    {
        services.AddScoped<IProjectTeamParticipantRepository, ProjectTeamParticipantRepository>();

        services.AddScoped<ProjectTeamParticipantUpdateCommand>();
        services.AddScoped<ProjectTeamParticipantAddUserParticipantCommand>();
        services.AddScoped<ProjectTeamParticipantDeleteUserParticipantCommand>();
        services.AddScoped<ProjectTeamParticipantLeaveCommand>();


        services.AddScoped<ProjectTeamParticipantFilterQuery>();
        services.AddScoped<ProjectFilterByUserParticipantIdQuery>();
        services.AddScoped<ProjectFilterByTeamParticipantIdQuery>();
    }

    public static void AddProjectTeamJoinRequestsInternal(this IServiceCollection services)
    {
        services.AddScoped<IProjectTeamJoinRequestRepository, ProjectTeamJoinRequestRepository>();

        services.AddScoped<ProjectTeamJoinRequestCreateCommand>();
        services.AddScoped<ProjectTeamJoinRequestDecideCommand>();
        services.AddScoped<ProjectTeamJoinRequestUpdateCommand>();

        services.AddScoped<ProjectTeamJoinRequestFilterQuery>();
    }

    public static void AddProjectTasksInternal(this IServiceCollection services)
    {
        services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();

        services.AddScoped<ProjectTaskCreateCommand>();
        services.AddScoped<ProjectTaskDeleteCommand>();
        services.AddScoped<ProjectTaskEditNameCommand>();
        services.AddScoped<ProjectTaskEditResponsibleUserCommand>();
        services.AddScoped<ProjectTaskEditDescriptionCommand>();
        services.AddScoped<ProjectTaskEditTagsCommand>();
        services.AddScoped<ProjectTaskEditLabelsCommand>();
        services.AddScoped<ProjectTaskEditTeamExecutorCommand>();
        services.AddScoped<ProjectTaskCloseCommand>();
        services.AddScoped<ProjectTaskOpenCommand>();

        services.AddScoped<ProjectTaskGetQuery>();
    }
}