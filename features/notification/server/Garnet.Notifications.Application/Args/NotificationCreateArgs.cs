namespace Garnet.Notifications.Application.Args
{
    public record NotificationCreateArgs(
        string Title,
        string Body,
        string Type,
        string UserId,
        DateTimeOffset CreatedAt,
        string? LinkedEntityId
    );
}