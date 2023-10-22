using Garnet.Common.Application;

namespace Garnet.Common.Infrastructure.MongoDb;

public record AuditInfoDocument(DateTimeOffset CreatedAt, string CreatedBy, DateTimeOffset UpdatedAt, string UpdatedBy, int Version)
{
    public static AuditInfoDocument Create(DateTimeOffset createdAt, string createdBy)
    {
        return new AuditInfoDocument(createdAt, createdBy, createdAt, createdBy, 0);
    }

    public static AuditInfo ToDomain(AuditInfoDocument doc)
    {
        return new AuditInfo(doc.CreatedAt, doc.CreatedBy, doc.UpdatedAt, doc.UpdatedBy, doc.Version);
    }
};