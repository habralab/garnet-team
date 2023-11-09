namespace Garnet.NewsFeed.Infrastructure.Api.NewsFeedPostCreate
{
    public record NewsFeedPostPayload(
        string Id,
        string TeamId,
        string AuthorUserId,
        DateTimeOffset CreatedAt,
        string Content);
}