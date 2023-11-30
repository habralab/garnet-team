using Garnet.Notifications.Application;
using Garnet.Notifications.Application.Args;

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
        public NotificationQuoteDocument[] QuoteDocuments { get; init; } = Array.Empty<NotificationQuoteDocument>();

        public static NotificationDocument Create(string id, NotificationCreateArgs args)
        {
            return new NotificationDocument()
            {
                Id = id,
                Title = args.Title,
                Body = args.Body,
                UserId = args.UserId,
                Type = args.Type,
                CreatedAt = args.CreatedAt,
                LinkedEntityId = args.LinkedEntityId,
                QuoteDocuments = args.QuotedEntities.Select(x=> NotificationQuoteDocument.Create(x)).ToArray()
            };
        }

        public static NotificationEntity ToDomain(NotificationDocument doc)
        {
            return new NotificationEntity(
                doc.Id,
                doc.Title,
                doc.Body,
                doc.Type,
                doc.UserId,
                doc.CreatedAt,
                doc.LinkedEntityId,
                doc.QuoteDocuments.Select(x => NotificationQuoteDocument.ToDomain(x)).ToArray()
            );
        }
    }
}