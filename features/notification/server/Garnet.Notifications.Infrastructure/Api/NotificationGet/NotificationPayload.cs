namespace Garnet.Notifications.Infrastructure.Api.NotificationGet
{
    public record NotificationPayload(
        string Title,
        string Body,
        string Type,
        string UserId,
        DateTimeOffset CreatedAt,
        string? LinkedEntityId
    );
}