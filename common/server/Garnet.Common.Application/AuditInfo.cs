namespace Garnet.Common.Application
{
    public record AuditInfo(DateTimeOffset CreatedAt, string CreatedBy, DateTimeOffset UpdatedAt, string UpdatedBy, int Version);
}