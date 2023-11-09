using Garnet.Common.Application;

namespace Garnet.NewsFeed.Application.NewsFeedPost
{
    public record NewsFeedPostEntity(
        string Id,
        string Content,
        string AuthorUserId,
        string TeamId,
        AuditInfo AuditInfo
    );
}