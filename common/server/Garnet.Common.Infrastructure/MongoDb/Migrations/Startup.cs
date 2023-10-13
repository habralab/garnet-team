using Microsoft.Extensions.DependencyInjection;

namespace Garnet.Common.Infrastructure.MongoDb.Migrations;

public static class Startup
{
    public static async Task ExecuteRepeatableMigrations(this IServiceProvider services, CancellationToken ct)
    {
        var migrations = services.GetRequiredService<IEnumerable<IRepeatableMigration>>();
        foreach (var migration in migrations)
        {
            await migration.Execute(ct);
        }
    }
}