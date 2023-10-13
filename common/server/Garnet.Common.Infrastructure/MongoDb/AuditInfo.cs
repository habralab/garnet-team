namespace Garnet.Common.Infrastructure.MongoDb;

public record AuditInfo(DateTimeOffset CreatedAt, string CreatedBy, DateTimeOffset UpdatedAt, string UpdatedBy);