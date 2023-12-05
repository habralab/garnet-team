using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Application;
using Garnet.Common.Infrastructure.Support;
using Garnet.NewsFeed.Infrastructure.Api;
using Garnet.NewsFeed.Infrastructure.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using SolidToken.SpecFlow.DependencyInjection;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.Infrastructure.Api;

namespace Garnet.NewsFeed.AcceptanceTests
{
    public static class Startup
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();
            
            services.AddNewsFeedInternal();

            services.AddScoped<NewsFeedQuery>();
            services.AddScoped<NewsFeedMutation>();

            services.AddScoped<CurrentUserProviderFake>();
            services.AddScoped<ICurrentUserProvider>(o => o.GetRequiredService<CurrentUserProviderFake>());

            services.AddScoped<QueryExceptionsContext>();

            services.AddScoped<DateTimeServiceFake>();
            services.AddScoped<IDateTimeService>(o => o.GetRequiredService<DateTimeServiceFake>());

            services.AddScoped<GiveMe>();
            services.AddScoped<StepsArgs>();

            services.AddCancellationTokenProvider();

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
        }

        private static void AddMessageBus(IServiceCollection services)
        {
            services.AddGarnetNewsFeedMessageBus(Uuid.NewGuid());
        }
    }
}