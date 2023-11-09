using Garnet.Common.Infrastructure.MongoDb;
using Garnet.NewsFeed.Application;

namespace Garnet.NewsFeed.Infrastructure.MongoDB
{
    public record NewsFeedPostDocument : DocumentBase
    {
        public string Id { get; init; } = null!;
        public string Content { get; init; } = null!;
        public string AuthorUserId { get; init; } = null!;

        public static NewsFeedPostDocument Create(string id, string content, string authorUserId)
        {
            return new NewsFeedPostDocument
            {
                Id = id,
                Content = content,
                AuthorUserId = authorUserId
            };
        }

        public static NewsFeedPostEntity ToDomain(NewsFeedPostDocument doc)
        {
            var audit = AuditInfoDocument.ToDomain(doc.AuditInfo);
            return new NewsFeedPostEntity(doc.Id, doc.Content, doc.AuthorUserId, audit);
        }
    }
}