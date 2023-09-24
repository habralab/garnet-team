namespace Garnet.Common.Infrastructure.Migrations;

public interface IRepeatableMigration
{
    Task Execute(CancellationToken ct);
}