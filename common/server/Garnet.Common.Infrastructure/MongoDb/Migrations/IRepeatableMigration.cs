namespace Garnet.Common.Infrastructure.MongoDb.Migrations;

public interface IRepeatableMigration
{
    Task Execute(CancellationToken ct);
}