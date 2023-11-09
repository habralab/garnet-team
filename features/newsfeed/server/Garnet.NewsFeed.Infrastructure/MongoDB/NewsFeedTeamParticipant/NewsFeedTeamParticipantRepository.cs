using Garnet.NewsFeed.Application.NewsFeedTeamParticipant;

namespace Garnet.NewsFeed.Infrastructure.MongoDB.NewsFeedTeamParticipant
{
    public class NewsFeedTeamParticipantRepository : INewsFeedTeamParticipantRepository
    {
        public Task<NewsFeedTeamParticipantEntity> CreateTeamParticipant(string id, string teamId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTeamParticipantById(string participantId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTeamParticipantsByTeam(string teamId)
        {
            throw new NotImplementedException();
        }
    }
}