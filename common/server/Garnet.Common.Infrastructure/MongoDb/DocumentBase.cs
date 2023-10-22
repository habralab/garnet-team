namespace Garnet.Common.Infrastructure.MongoDb;

public record DocumentBase
{
    public AuditInfoDocument AuditInfo { get; init; } = null!;
}