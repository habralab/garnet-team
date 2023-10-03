using System.Diagnostics.CodeAnalysis;
using Garnet.Common.Infrastructure.Migrations;
using Garnet.Teams.Application;
using Garnet.Common.Infrastructure.MessageBus;
using Garnet.Teams.Infrastructure.Api;
using Garnet.Teams.Infrastructure.MongoDb;
using Garnet.Teams.Infrastructure.MongoDb.Migration;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Garnet.Users.Events;
using Garnet.Teams.Infrastructure.EventHandlers;

namespace Garnet.Team
{
    [ExcludeFromCodeCoverage]
    public static class Startup
    {
        public static IRequestExecutorBuilder AddGarnetTeams(this IRequestExecutorBuilder builder)
        {
            builder.AddType<TeamsMutation>();
            builder.AddType<TeamsQuery>();
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
            services.AddScoped<TeamService>();
            services.AddScoped<TeamUserService>();
            services.AddScoped<TeamMembershipService>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ITeamParticipantRepository, TeamParticipantRepository>();
            services.AddScoped<ITeamUserRepository, TeamUserRepository>();
        }
        private static void AddRepeatableMigrations(this IServiceCollection services)
        {
            services.AddScoped<IRepeatableMigration, CreateIndexesTeamMigration>();
        }

        public static void AddGarnetTeamsMessageBus(this IServiceCollection services, string name)
        {
            services.AddGarnetMessageBus(name, o =>
            {
                o.RegisterConsumer<UserCreatedEventConsumer, UserCreatedEvent>();
            });
        }
    }
}