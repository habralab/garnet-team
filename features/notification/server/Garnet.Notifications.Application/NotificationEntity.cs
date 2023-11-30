namespace Garnet.Notifications.Application
{
    public record NotificationEntity(
        string Id,
        string Title,
        string Body,
        string Type,
        string UserId,
        DateTimeOffset CreatedAt,
        string? LinkedEntityId,
        QuotedEntity[] QuotedEntities
    );
}