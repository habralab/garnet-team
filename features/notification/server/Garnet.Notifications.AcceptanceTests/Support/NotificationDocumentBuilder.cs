using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.Support;
using Garnet.Notifications.Application;
using Garnet.Notifications.Application.Args;
using Garnet.Notifications.Events;
using Garnet.Notifications.Infrastructure.MongoDB;

namespace Garnet.Notifications.AcceptanceTests.Support
{
    public class NotificationDocumentBuilder
    {
        private readonly string _id = Uuid.NewMongo();
        private readonly string _title = "Title";
        private readonly string _body = "Body";
        private DateTimeOffset _createdAt = DateTimeOffset.Now;
        private string _userId = "system";
        private readonly string _type = "Type";

        public NotificationDocumentBuilder WithUserId(string userId)
        {
            _userId = userId;
            return this;
        }

        public NotificationDocumentBuilder WithCreatedAt(DateTimeOffset createdAt)
        {
            _createdAt = createdAt;
            return this;
        }

        public NotificationDocument Build()
        {
            return NotificationDocument.Create(
                _id,
                new NotificationCreateArgs(_title, _body, _type, _userId, _createdAt, null, Array.Empty<QuotedEntity>())
            );
        }

        public static implicit operator NotificationDocument(NotificationDocumentBuilder builder)
        {
            return builder.Build();
        }
    }

    public static class GiveMeExtensions
    {
        public static NotificationDocumentBuilder Notification(this GiveMe _)
        {
            return new NotificationDocumentBuilder();
        }

        public static SendNotificationCommandMessage EventFromNotification(this GiveMe _, NotificationDocument document)
        {
            return new SendNotificationCommandMessage(
                document.Title,
                document.Body,
                document.UserId,
                document.Type,
                document.CreatedAt,
                document.LinkedEntityId,
                Array.Empty<NotificationQuotedEntity>()
            );
        }
    }
}