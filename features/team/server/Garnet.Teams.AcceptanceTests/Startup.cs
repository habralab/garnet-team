using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Application;
using Garnet.Common.Infrastructure.Migrations;
using Garnet.Common.Infrastructure.Support;
using Garnet.Team;
using Garnet.Teams.Infrastructure.Api;
using Garnet.Teams.Infrastructure.MongoDb;
using Garnet.Teams.Infrastructure.MongoDb.Migration;
using Garnet.Common.Infrastructure.MessageBus;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using SolidToken.SpecFlow.DependencyInjection;
using Garnet.Teams.AcceptanceTests.FakeServices.ProjectFake;
using Garnet.Teams.Events.TeamJoinProjectRequest;

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

            services.AddTeamInternal();
            services.AddTeamUserInternal();
            services.AddTeamParticipantInternal();
            services.AddTeamUserJoinRequestInternal();
            services.AddTeamJoinInvitationInternal();
            services.AddTeamJoinProjectRequestInternal();

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
            services.AddGarnetMessageBus(Uuid.NewGuid(), o =>
            {
                o.RegisterConsumer<ProjectTeamJoinRequestFakeConsumer, TeamJoinProjectRequestCreatedEvent>();
            });
        }
    }
}