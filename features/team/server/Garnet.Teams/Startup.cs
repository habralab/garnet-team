using Garnet.Common.Infrastructure.Migrations;
using Garnet.Teams.Application;
using Garnet.Teams.Infrastructure.Api;
using Garnet.Teams.Infrastructure.MongoDb;
using Garnet.Teams.Infrastructure.MongoDb.Migration;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Garnet.Team
{
    public static class Startup
    {
        public static IRequestExecutorBuilder AddGarnetTeams(this IRequestExecutorBuilder builder)
        {
            builder.AddType<TeamsMutation>();
            builder.Services.AddGarnetTeamsInternal();
            builder.Services.AddRepeatableMigrations();

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
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ITeamParticipantRepository, TeamParticipantRepository>();
        }
        private static void AddRepeatableMigrations(this IServiceCollection services)
        {
            services.AddScoped<IRepeatableMigration, CreateIndexesTeamMigration>();
        }
    }
}