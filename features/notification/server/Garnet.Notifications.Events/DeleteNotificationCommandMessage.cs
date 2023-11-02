namespace Garnet.Notifications.Events
{
    public record DeleteNotificationCommandMessage(
        string UserId,
        string Type,
        string? LinkedEntityId
    );
}