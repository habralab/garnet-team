using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Application;
using Garnet.Common.Infrastructure.Migrations;
using Garnet.Common.Infrastructure.Support;
using Garnet.Team;
using Garnet.Teams.Application;
using Garnet.Teams.Infrastructure.Api;
using Garnet.Teams.Infrastructure.MongoDb;
using Garnet.Teams.Infrastructure.MongoDb.Migration;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using SolidToken.SpecFlow.DependencyInjection;

namespace Garnet.Teams.AcceptanceTests
{
    public static class Startup
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            services.AddScoped<CurrentUserProviderFake>();
            services.AddScoped<ICurrentUserProvider>(o => o.GetRequiredService<CurrentUserProviderFake>());

            services.AddScoped<ITeamParticipantRepository, TeamParticipantRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ITeamUserRepository, TeamUserRepository>();
            services.AddScoped<ITeamUserJoinRequestRepository, TeamUserJoinRequestRepository>();

            services.AddScoped<TeamDeleteUnitOfWork>();

            services.AddScoped<TeamService>();
            services.AddScoped<TeamUserService>();
            services.AddScoped<TeamParticipantService>();
            services.AddScoped<TeamUserJoinRequestService>();

            services.AddScoped<TeamsMutation>();
            services.AddScoped<TeamsQuery>();

            services.AddScoped<QueryExceptionsContext>();

            services.AddScoped<GiveMe>();
            services.AddScoped<StepsArgs>();

            AddMongoDb(services);
            AddMessageBus(services);

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

            services.AddScoped<IRepeatableMigration, CreateIndexesTeamUserMigration>();
            services.AddScoped<IRepeatableMigration, CreateIndexesTeamMigration>();
        }

        private static void AddMessageBus(IServiceCollection services)
        {
            services.AddGarnetTeamsMessageBus(Uuid.NewGuid());
        }
    }
}