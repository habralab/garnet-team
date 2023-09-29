using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Application;
using Garnet.Common.Infrastructure.Support;
using Garnet.User;
using Garnet.Users.Application;
using Garnet.Users.Infrastructure.Api;
using Garnet.Users.Infrastructure.MongoDb;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using SolidToken.SpecFlow.DependencyInjection;

namespace Garnet.Users.AcceptanceTests;

public static class Startup
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();
        
        AddMongoDb(services);
        AddMessageBus(services);

        services.AddScoped<CurrentUserProviderFake>();
        services.AddScoped<ICurrentUserProvider>(o => o.GetRequiredService<CurrentUserProviderFake>());
    
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<UsersService>();

        services.AddScoped<UsersQuery>();
        services.AddScoped<UsersMutation>();

        services.AddScoped<GiveMe>();
        services.AddScoped<StepsArgs>();

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

        services.AddRepeatableMigrations();
    }

    private static void AddMessageBus(IServiceCollection services)
    {
        services.AddGarnetUsersMessageBus(Uuid.NewGuid());
    }
}