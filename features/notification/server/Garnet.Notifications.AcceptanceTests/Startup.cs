using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Application;
using Garnet.Common.Infrastructure.Support;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using Garnet.Notifications;
using SolidToken.SpecFlow.DependencyInjection;

namespace Garnet.Notifications.AcceptanceTests
{
    public static class Startup
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            services.AddNotificationInternal();

            services.AddScoped<CurrentUserProviderFake>();
            services.AddScoped<ICurrentUserProvider>(o => o.GetRequiredService<CurrentUserProviderFake>());

            services.AddScoped<DateTimeServiceFake>();
            services.AddScoped<IDateTimeService>(o => o.GetRequiredService<DateTimeServiceFake>());

            services.AddScoped<GiveMe>();
            services.AddScoped<StepsArgs>();

            AddMongoDb(services);
            AddMessageBus(services);

            return services;
        }

        private static void AddMongoDb(IServiceCollection services)
        {
            services.AddScoped<MongoDbRunner>(_ => MongoDbRunner.Start());
        }

        private static void AddMessageBus(IServiceCollection services)
        {
            services.AddGarnetNotificationMessageBus(Uuid.NewGuid());
        }
    }
}