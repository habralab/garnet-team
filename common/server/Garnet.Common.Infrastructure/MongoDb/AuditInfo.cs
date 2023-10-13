namespace Garnet.Common.Infrastructure.MongoDb;

public record AuditInfo(DateTimeOffset CreatedAt, string CreatedBy, DateTimeOffset UpdatedAt, string UpdatedBy, int Version)
{
    public static AuditInfo Create(DateTimeOffset createdAt, string createdBy)
    {
        return new AuditInfo(createdAt, createdBy, createdAt, createdBy, 0);
    }
};