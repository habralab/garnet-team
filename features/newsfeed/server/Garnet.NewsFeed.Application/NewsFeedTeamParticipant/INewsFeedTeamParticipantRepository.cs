namespace Garnet.NewsFeed.Application.NewsFeedTeamParticipant
{
    public interface INewsFeedTeamParticipantRepository
    {
        Task<NewsFeedTeamParticipantEntity> CreateTeamParticipant(string id, string teamId, string userId);
        Task DeleteTeamParticipantById(string participantId);
        Task DeleteTeamParticipantsByTeam(string teamId);
    }
}