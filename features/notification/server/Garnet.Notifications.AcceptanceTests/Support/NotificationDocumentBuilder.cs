using Garnet.Common.AcceptanceTests.Support;
using Garnet.Common.Infrastructure.Support;
using Garnet.Notifications.Infrastructure.MongoDB;

namespace Garnet.Notifications.AcceptanceTests.Support
{
    public class NotificationDocumentBuilder
    {
        private string _id = Uuid.NewMongo();
        private string _title = "Title";
        private string _body = "Body";
        private DateTimeOffset _createdAt = DateTimeOffset.Now;
        private string _userId = "system";
        private string _type = "Type";
        private string? _linkedEntityId;

        public NotificationDocumentBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public NotificationDocumentBuilder WithBody(string body)
        {
            _body = body;
            return this;
        }

        public NotificationDocumentBuilder WithUserId(string userId)
        {
            _userId = userId;
            return this;
        }

        public NotificationDocumentBuilder WithType(string type)
        {
            _type = type;
            return this;
        }

        public NotificationDocumentBuilder WithLinkedEntityId(string linkedEntityId)
        {
            _linkedEntityId = linkedEntityId;
            return this;
        }

        public NotificationDocument Build()
        {
            return NotificationDocument.Create(_id, _title, _body, _userId, _type, _createdAt, _linkedEntityId);
        }

        public static implicit operator NotificationDocument(NotificationDocumentBuilder builder)
        {
            return builder.Build();
        }
    }

    public static class GiveMeExtensions
    {
        public static NotificationDocumentBuilder User(this GiveMe _)
        {
            return new NotificationDocumentBuilder();
        }
    }
}