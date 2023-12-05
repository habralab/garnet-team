using Garnet.NewsFeed.Application.NewsFeedPost.Args;

namespace Garnet.NewsFeed.Application.NewsFeedPost
{
    public interface INewsFeedPostRepository
    {
        Task<NewsFeedPostEntity> CreatePost(NewsFeedPostCreateArgs args);
        Task<NewsFeedPostEntity[]> GetPostList(string teamId, int skip, int take);
        Task DeletePostsByTeam(string teamId);
    }
}