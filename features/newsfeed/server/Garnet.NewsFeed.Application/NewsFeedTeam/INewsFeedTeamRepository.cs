namespace Garnet.NewsFeed.Application.NewsFeedTeam
{
    public interface INewsFeedTeamRepository
    {
        Task CreateTeam(string id, string ownerUserId);
        Task<NewsFeedTeamEntity?> GetTeamById(string id);
        Task DeleteTeam(string id);
    }
}