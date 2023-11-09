using System.Diagnostics.CodeAnalysis;
using Garnet.Common.Infrastructure.Support;
using HotChocolate.Execution.Configuration;
using Garnet.NewsFeed.Infrastructure.MongoDB;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Common.Infrastructure.MessageBus;
using Garnet.NewsFeed.Infrastructure.Api;
using Garnet.Common.Infrastructure.Api;
using Microsoft.Extensions.DependencyInjection;
using Garnet.Common.Infrastructure.MongoDb.Migrations;
using Garnet.NewsFeed.Infrastructure.MongoDB.Migration;
using Garnet.NewsFeed.Application;

namespace Garnet.NewsFeed
{
    [ExcludeFromCodeCoverage]
    public static class Startup
    {

        public static IRequestExecutorBuilder AddGarnetNewsFeed(this IRequestExecutorBuilder builder)
        {
            builder.Services.AddGarnetAuthorization();
            builder.AddApiType<NewsFeedMutation>();
            builder.AddApiType<NewsFeedQuery>();
            builder.Services.AddGarnetNewsFeedInternal();
            builder.Services.AddGarnetNewsFeedMessageBus(nameof(NewsFeed));
            builder.Services.AddRepeatableMigrations();
            return builder;
        }

        private static void AddGarnetNewsFeedInternal(this IServiceCollection services)
        {
            services.AddScoped<DbFactory>(o => new DbFactory(EnvironmentEx.GetRequiredEnvironmentVariable("MONGO_CONNSTRING")));
            services.AddGarnetMongoSerializers();
            services.AddNewsFeedInternal();
        }

        public static void AddNewsFeedInternal(this IServiceCollection services)
        {
            services.AddScoped<INewsFeedPostRepository, NewsFeedPostRepository>();
        }


        public static void AddGarnetNewsFeedMessageBus(this IServiceCollection services, string name)
        {
            services.AddGarnetMessageBus(name, o =>
            {
            }
            );
        }

        public static void AddRepeatableMigrations(this IServiceCollection services)
        {
            services.AddScoped<IRepeatableMigration, CreateIndexesNewsFeedPostMigration>();
        }
    }
}