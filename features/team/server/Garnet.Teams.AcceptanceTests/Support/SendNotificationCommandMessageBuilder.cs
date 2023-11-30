using Garnet.Notifications.Events;

namespace Garnet.Teams.AcceptanceTests.Support
{
    public class SendNotificationCommandMessageBuilder
    {
        private string _userId = "system";
        private string _type = "event";
        private string? _linkedEntityId;
        private string[] _quotedEntitiesIds = Array.Empty<string>();

        public SendNotificationCommandMessageBuilder WithUserId(string userId)
        {
            _userId = userId;
            return this;
        }

        public SendNotificationCommandMessageBuilder WithType(string type)
        {
            _type = type;
            return this;
        }

        public SendNotificationCommandMessageBuilder WithLinkedEntityId(string linkedEntityId)
        {
            _linkedEntityId = linkedEntityId;
            return this;
        }

        public SendNotificationCommandMessageBuilder WithQuotedEntityIds(string[] quotedEntitiesIds)
        {
            _quotedEntitiesIds = quotedEntitiesIds;
            return this;
        }

        public SendNotificationCommandMessage Build()
        {
            var quotes = _quotedEntitiesIds.Select(x => new NotificationQuotedEntity(x, string.Empty, x));
            return new SendNotificationCommandMessage(
                "Title",
                "Body",
                _userId,
                _type,
                DateTimeOffset.Now,
                _linkedEntityId,
                quotes.ToArray());
        }

        public static implicit operator SendNotificationCommandMessage(SendNotificationCommandMessageBuilder builder)
        {
            return builder.Build();
        }
    }
}