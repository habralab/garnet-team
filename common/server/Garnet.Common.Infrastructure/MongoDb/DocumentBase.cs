namespace Garnet.Common.Infrastructure.MongoDb;

public record DocumentBase
{
    public AuditInfo AuditInfo { get; init; } = null!;
}