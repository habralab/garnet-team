using Garnet.Notifications.Infrastructure.Api.NotificationGet;

namespace Garnet.Notifications.Infrastructure.Api.NotificationDelete
{
    public record NotificationDeletePayload(
        string Title,
        string Body,
        string Type,
        string UserId,
        DateTimeOffset CreatedAt,
        string? LinkedEntityId
    ) : NotificationPayload(Title, Body, Type, UserId, CreatedAt, LinkedEntityId);
}