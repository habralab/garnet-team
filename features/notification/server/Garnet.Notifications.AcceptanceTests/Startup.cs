using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Application;
using Garnet.Common.Infrastructure.Support;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using SolidToken.SpecFlow.DependencyInjection;
using Garnet.Notifications.Infrastructure.MongoDB;
using Garnet.Notifications.AcceptanceTests.FakeServices;
using Garnet.Common.Infrastructure.MessageBus;
using Garnet.Notifications.Events;
using Garnet.Notifications.Infrastructure.Api;

namespace Garnet.Notifications.AcceptanceTests
{
    public static class Startup
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            services.AddNotificationInternal();

            services.AddScoped<NotificationQuery>();
            services.AddScoped<NotificationMutation>();

            services.AddScoped<CurrentUserProviderFake>();
            services.AddScoped<ICurrentUserProvider>(o => o.GetRequiredService<CurrentUserProviderFake>());

            services.AddScoped<FakeNotificationProducer>();

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
            services.AddScoped<DbFactory>(o =>
            {
                var mongo = o.GetRequiredService<MongoDbRunner>();
                return new DbFactory(mongo.ConnectionString);
            });
            services.AddScoped<Db>(o => o.GetRequiredService<DbFactory>().Create());
        }

        private static void AddMessageBus(IServiceCollection services)
        {
            services.AddGarnetNotificationMessageBus(Uuid.NewGuid());
            services.AddGarnetMessageBus(Uuid.NewGuid(), o =>
            {
                o.RegisterMessage<SendNotificationCommandMessage>();
            });
        }
    }
}