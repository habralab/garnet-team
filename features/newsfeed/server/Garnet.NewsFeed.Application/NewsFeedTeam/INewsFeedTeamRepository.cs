namespace Garnet.NewsFeed.Application.NewsFeedTeam
{
    public interface INewsFeedTeamRepository
    {
        Task<NewsFeedTeamEntity> CreateTeam(string id, string ownerUserId);
        Task DeleteTeam(string id);
    }
}