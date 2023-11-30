namespace Garnet.Notifications.Infrastructure.Api.NotificationGet
{
    public record QuotedEntityPayload(
        string Id,
        string AvatarUrl,
        string Quote
    );
}