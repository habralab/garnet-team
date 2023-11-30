using Garnet.Notifications.Infrastructure.Api.NotificationGet;

namespace Garnet.Notifications.Infrastructure.Api.NotificationDelete
{
    public record NotificationDeletePayload(
        string Id,
        string Title,
        string Body,
        string Type,
        string UserId,
        DateTimeOffset CreatedAt,
        string? LinkedEntityId,
        QuotedEntityPayload[] QuotedEntities
    ) : NotificationPayload(Id, Title, Body, Type, UserId, CreatedAt, LinkedEntityId, QuotedEntities);
}