using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;

namespace Garnet.Projects.AcceptanceTests;

public static class Startup
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();

        return services;
    }
}