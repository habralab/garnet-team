namespace Garnet.NewsFeed.Application.NewsFeedTeam
{
    public interface INewsFeedTeamRepository
    {
        Task<NewsFeedTeamEntity> CreateTeam(string id, string ownerUserId);
        Task<NewsFeedTeamEntity?> GetTeamById(string id);
        Task DeleteTeam(string id);
    }
}