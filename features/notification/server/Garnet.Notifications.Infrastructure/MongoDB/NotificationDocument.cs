namespace Garnet.Notifications.Infrastructure.MongoDB
{
    public class NotificationDocument
    {
        public string Id { get; init; } = null!;
        public string Title { get; init; } = null!;
        public string Body { get; init; } = null!;
        public DateTimeOffset CreatedAt { get; init; }
        public string UserId { get; init; } = null!;
        public string Type { get; init; } = null!;
        public string? LinkedEntityId { get; init; }

        public static NotificationDocument Create(string id, string title, string body, string userId, string type, DateTimeOffset createdAt, string? linkedEntityId)
        {
            return new NotificationDocument()
            {
                Id = id,
                Title = title,
                Body = body,
                UserId = userId,
                Type = type,
                CreatedAt = createdAt,
                LinkedEntityId = linkedEntityId
            };
        }
    }
}