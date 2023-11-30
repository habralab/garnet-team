namespace Garnet.Notifications.Events
{
    public record SendNotificationCommandMessage(
        string Title,
        string Body,
        string UserId,
        string Type,
        DateTimeOffset CreatedAt,
        string? LinkedEntityId,
        NotificationQuotedEntity[] QuotedEntities
    );
}