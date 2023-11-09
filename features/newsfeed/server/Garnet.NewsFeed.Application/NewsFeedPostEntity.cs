using Garnet.Common.Application;

namespace Garnet.NewsFeed.Application
{
    public record NewsFeedPostEntity(
        string Id,
        string Content,
        string AuthorUserId,
        AuditInfo AuditInfo
    );
}