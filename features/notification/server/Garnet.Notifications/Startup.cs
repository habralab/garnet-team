using System.Diagnostics.CodeAnalysis;
using Garnet.Common.Infrastructure.MongoDb;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Garnet.Common.Infrastructure.MessageBus;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Notifications.Infrastructure.MongoDB;
using Garnet.Common.Infrastructure.Support;
using Garnet.Notifications.Infrastructure.EventHandlers;
using Garnet.Notifications.Events;

namespace Garnet.Notifications
{
    [ExcludeFromCodeCoverage]

    public static class Startup
    {
        public static IRequestExecutorBuilder AddGarnetNotification(this IRequestExecutorBuilder builder)
        {
            builder.Services.AddGarnetAuthorization();
            builder.Services.AddGarnetNotificationInternal();
            builder.Services.AddGarnetNotificationMessageBus(nameof(Notifications));
            builder.Services.AddRepeatableMigrations();
            return builder;
        }

        private static void AddGarnetNotificationInternal(this IServiceCollection services)
        {
            services.AddScoped<DbFactory>(o => new DbFactory(EnvironmentEx.GetRequiredEnvironmentVariable("MONGO_CONNSTRING")));
            services.AddGarnetMongoSerializers();
        }

        public static void AddNotificationInternal(this IServiceCollection services)
        {

        }

        public static void AddGarnetNotificationMessageBus(this IServiceCollection services, string name)
        {
            services.AddGarnetMessageBus(name, o =>
            {
                o.RegisterConsumer<SendNotificationCommandMessageConsumer, SendNotificationCommandMessage>();
            });
        }

        public static void AddRepeatableMigrations(this IServiceCollection services)
        {

        }
    }
}