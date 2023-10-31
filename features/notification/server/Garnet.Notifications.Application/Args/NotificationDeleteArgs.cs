namespace Garnet.Notifications.Application.Args
{
    public record NotificationDeleteArgs(
        string UserId,
        string Type,
        string? LinkedEntityId
    );
}