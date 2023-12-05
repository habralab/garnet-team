using Garnet.Common.Infrastructure.MongoDb;
using Garnet.NewsFeed.Application.NewsFeedPost;

namespace Garnet.NewsFeed.Infrastructure.MongoDB.NewsFeedPost
{
    public record NewsFeedPostDocument : DocumentBase
    {
        public string Id { get; init; } = null!;
        public string TeamId { get; init; } = null!;
        public string Content { get; init; } = null!;

        public static NewsFeedPostDocument Create(string id, string content, string teamId)
        {
            return new NewsFeedPostDocument
            {
                Id = id,
                TeamId = teamId,
                Content = content
            };
        }

        public static NewsFeedPostEntity ToDomain(NewsFeedPostDocument doc)
        {
            var audit = AuditInfoDocument.ToDomain(doc.AuditInfo);
            return new NewsFeedPostEntity(doc.Id, doc.Content, doc.TeamId, audit);
        }
    }
}