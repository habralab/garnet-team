using Garnet.Notifications.Application;

namespace Garnet.Notifications.Infrastructure.MongoDB
{
    public class NotificationQuoteDocument
    {
        public string Id { get; init; } = null!;
        public string AvatarUrl { get; init; } = null!;
        public string Quote { get; init; } = null!;

        public static QuotedEntity ToDomain(NotificationQuoteDocument doc)
        {
            return new QuotedEntity(
                doc.Id,
                doc.AvatarUrl,
                doc.Quote
            );
        }

        public static NotificationQuoteDocument Create(QuotedEntity entity)
        {
            return new NotificationQuoteDocument() {
                Id = entity.Id,
                AvatarUrl = entity.AvatarUrl,
                Quote = entity.Quote
            };
        }
    }
}