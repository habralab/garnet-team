namespace Garnet.Notifications.Events
{
    public record NotificationQuotedEntity(
        string Id,
        string AvatarUrl,
        string Quote
    );
}