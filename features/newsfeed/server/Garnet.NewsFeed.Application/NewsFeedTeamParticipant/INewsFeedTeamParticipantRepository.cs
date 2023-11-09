namespace Garnet.NewsFeed.Application.NewsFeedTeamParticipant
{
    public interface INewsFeedTeamParticipantRepository
    {
        Task CreateTeamParticipant(string id, string teamId, string userId);
        Task<NewsFeedTeamParticipantEntity?> EnsureUserIsTeamParticipant(string teamId, string userId);
        Task DeleteTeamParticipantById(string participantId);
        Task DeleteTeamParticipantsByTeam(string teamId);
    }
}