using System.Diagnostics.CodeAnalysis;
using Garnet.Common.Application;
using Garnet.Common.Infrastructure;
using Garnet.Common.Infrastructure.Api;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Common.Infrastructure.MessageBus;
using Garnet.Common.Infrastructure.MongoDb.Migrations;
using Garnet.Teams.Infrastructure.Api;
using Garnet.Teams.Infrastructure.MongoDb;
using Garnet.Teams.Infrastructure.MongoDb.Migration;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Garnet.Users.Events;
using Garnet.Projects.Events.Project;
using Garnet.Projects.Events.ProjectTeamJoinRequest;
using Garnet.Teams.Application.TeamJoinProjectRequest.Commands;
using Garnet.Teams.Application.TeamJoinInvitation.Commands;
using Garnet.Teams.Application.Team;
using Garnet.Teams.Application.TeamUser;
using Garnet.Teams.Application.TeamUserJoinRequest;
using Garnet.Teams.Application.TeamParticipant;
using Garnet.Teams.Application.TeamJoinProjectRequest;
using Garnet.Teams.Application.TeamJoinInvitation;
using Garnet.Teams.Infrastructure.EventHandlers.User;
using Garnet.Teams.Infrastructure.EventHandlers.Project;
using Garnet.Teams.Infrastructure.MongoDb.Team;
using Garnet.Teams.Infrastructure.MongoDb.TeamParticipant;
using Garnet.Teams.Infrastructure.MongoDb.TeamUser;
using Garnet.Teams.Infrastructure.MongoDb.TeamUserJoinRequest;
using Garnet.Teams.Infrastructure.MongoDb.TeamJoinProjectRequest;
using Garnet.Teams.Infrastructure.MongoDb.TeamJoinInvitation;
using Garnet.Teams.Events.Team;
using Garnet.Teams.Events.TeamUserJoinRequest;
using Garnet.Teams.Events.TeamJoinInvitation;
using Garnet.Teams.Events.TeamJoinProjectRequest;
using Garnet.Teams.Application.Team.Commands;
using Garnet.Teams.Application.Team.Queries;
using Garnet.Teams.Application.TeamUserJoinRequest.Commands;
using Garnet.Teams.Application.TeamUserJoinRequest.Queries;
using Garnet.Teams.Application.TeamParticipant.Queries;
using Microsoft.AspNetCore.Http;

namespace Garnet.Team
{
    [ExcludeFromCodeCoverage]
    public static class Startup
    {
        public static IRequestExecutorBuilder AddGarnetTeams(this IRequestExecutorBuilder builder)
        {
            builder.AddApiType<TeamsMutation>();
            builder.AddApiType<TeamsQuery>();
            builder.Services.AddCurrentUserProvider();
            builder.Services.AddGarnetTeamsInternal();
            builder.Services.AddRepeatableMigrations();
            builder.Services.AddGarnetTeamsMessageBus(nameof(Teams));

            return builder;
        }

        private static void AddGarnetTeamsInternal(this IServiceCollection services)
        {
            const string mongoConnStringEnv = "MONGO_CONNSTRING";
            var mongoDbConnString =
                Environment.GetEnvironmentVariable(mongoConnStringEnv)
                ?? throw new Exception($"No {mongoConnStringEnv} environment variable was provided.");
            services.AddScoped<DbFactory>(o => new DbFactory(mongoDbConnString));

            services.AddTeamInternal();
            services.AddTeamUserInternal();
            services.AddTeamParticipantInternal();
            services.AddTeamUserJoinRequestInternal();
            services.AddTeamJoinInvitationInternal();
            services.AddTeamJoinProjectRequestInternal();
        }
        private static void AddRepeatableMigrations(this IServiceCollection services)
        {
            services.AddScoped<IRepeatableMigration, CreateIndexesTeamParticipantMigration>();
            services.AddScoped<IRepeatableMigration, CreateIndexesTeamMigration>();
        }

        public static void AddGarnetTeamsMessageBus(this IServiceCollection services, string name)
        {
            services.AddGarnetMessageBus(name, o =>
            {
                o.RegisterConsumer<UserCreatedEventConsumer, UserCreatedEvent>();
                o.RegisterConsumer<UserUpdatedEventConsumer, UserUpdatedEvent>();
                o.RegisterConsumer<ProjectTeamJoinRequestDecidedEventConsumer, ProjectTeamJoinRequestDecidedEvent>();
                o.RegisterConsumer<ProjectDeletedEventConsumer, ProjectDeletedEvent>();
                o.RegisterMessage<TeamCreatedEvent>();
                o.RegisterMessage<TeamDeletedEvent>();
                o.RegisterMessage<TeamUpdatedEvent>();
                o.RegisterMessage<TeamUserJoinRequestCreatedEvent>();
                o.RegisterMessage<TeamJoinInvitationCreatedEvent>();
                o.RegisterMessage<TeamUserJoinRequestDecidedEvent>();
                o.RegisterMessage<TeamJoinProjectRequestCreatedEvent>();
            });
        }

        public static void AddTeamInternal(this IServiceCollection services)
        {
            services.AddScoped<ITeamRepository, TeamRepository>();

            services.AddScoped<TeamCreateCommand>();
            services.AddScoped<TeamDeleteCommand>();
            services.AddScoped<TeamEditDescriptionCommand>();
            services.AddScoped<TeamEditOwnerCommand>();
            services.AddScoped<TeamUploadAvatarCommand>();

            services.AddScoped<TeamGetQuery>();
            services.AddScoped<TeamsFilterQuery>();
        }

        public static void AddTeamUserInternal(this IServiceCollection services)
        {
            services.AddScoped<ITeamUserRepository, TeamUserRepository>();
        }

        public static void AddTeamParticipantInternal(this IServiceCollection services)
        {
            services.AddScoped<ITeamParticipantRepository, TeamParticipantRepository>();
            
            services.AddScoped<TeamParticipantFilterQuery>();
        }

        public static void AddTeamUserJoinRequestInternal(this IServiceCollection services)
        {
            services.AddScoped<ITeamUserJoinRequestRepository, TeamUserJoinRequestRepository>();

            services.AddScoped<TeamUserJoinRequestCreateCommand>();
            services.AddScoped<TeamUserJoinRequestDecideCommand>();

            services.AddScoped<TeamUserJoinRequestsShowQuery>();
        }

        public static void AddTeamJoinInvitationInternal(this IServiceCollection services)
        {
            services.AddScoped<ITeamJoinInvitationRepository, TeamJoinInvitationRepository>();
            services.AddScoped<TeamJoinInviteCommand>();
        }

        public static void AddTeamJoinProjectRequestInternal(this IServiceCollection services)
        {
            services.AddScoped<ITeamJoinProjectRequestRepository, TeamJoinProjectRequestRepository>();
            services.AddScoped<TeamJoinProjectRequestCreateCommand>();
        }
    }
}