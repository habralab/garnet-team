using Garnet.NewsFeed.Application.NewsFeedTeam;

namespace Garnet.NewsFeed.Infrastructure.MongoDB.NewsFeedTeam
{
    public class NewsFeedTeamRepository : INewsFeedTeamRepository
    {
        public Task<NewsFeedTeamEntity> CreateTeam(string id, string ownerUserId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTeam(string id)
        {
            throw new NotImplementedException();
        }
    }
}