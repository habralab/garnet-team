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
using Garnet.Notifications.Infrastructure.Api;
using Garnet.Common.Infrastructure.Api;
using Garnet.Notifications.Application;
using Garnet.Notifications.Application.Queries;

namespace Garnet.Notifications
{
    [ExcludeFromCodeCoverage]

    public static class Startup
    {
        public static IRequestExecutorBuilder AddGarnetNotifications(this IRequestExecutorBuilder builder)
        {
            builder.Services.AddGarnetAuthorization();
            builder.AddApiType<NotificationsQuery>();
            builder.AddApiType<NotificationsMutation>();
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
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<NotificationsGetListByCurrentUserQuery>();
        }

        public static void AddGarnetNotificationMessageBus(this IServiceCollection services, string name)
        {
            services.AddGarnetMessageBus(name, o =>
            {
                o.RegisterConsumer<SendNotificationCommandMessageConsumer, SendNotificationCommandMessage>();
                o.RegisterConsumer<DeleteNotificationCommandMessageConsumer, DeleteNotificationCommandMessage>();
            });
        }

        public static void AddRepeatableMigrations(this IServiceCollection services)
        {

        }
    }
}