namespace Garnet.NewsFeed.Application
{
    public interface INewsFeedPostRepository
    {
        Task<NewsFeedPostEntity> CreatePost(string content, string authorUserId);
        Task<NewsFeedPostEntity[]> GetPostList(string teamId, int skip, int take);
    }
}