using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using Garnet.Common.Infrastructure.Migrations;

namespace Garnet.Teams.AcceptanceTests
{
    [Binding]
    public class ScenarioDecorator
    {
        [BeforeScenario]
        public async Task BeforeScenario(ScenarioContext context)
        {
            var services = context.ScenarioContainer.Resolve<IServiceProvider>();
            await services.ExecuteRepeatableMigrations(CancellationToken.None);
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext context)
        {
            var services = context.ScenarioContainer.Resolve<IServiceProvider>();
            var mongo = services.GetRequiredService<MongoDbRunner>();
            mongo.Dispose();
        }
    }
}