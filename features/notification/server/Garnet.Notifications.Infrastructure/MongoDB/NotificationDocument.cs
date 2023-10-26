namespace Garnet.Notifications.Infrastructure.MongoDB
{
    public class NotificationDocument
    {
        public string Id { get; init; } = null!;
        public string Title { get; init; } = null!;
        public string Body { get; init; } = null!;
        public string UserId { get; init; } = null!;
        public string Type { get; init; } = null!;
        public string? LinkedEntityId { get; init; }
    }
}