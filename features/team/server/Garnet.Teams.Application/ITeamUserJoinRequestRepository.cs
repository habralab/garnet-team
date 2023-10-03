namespace Garnet.Teams.Application
{
    public interface ITeamUserJoinRequestRepository
    {
        Task<TeamUserJoinRequest> CreateJoinRequestByUser(CancellationToken ct, string userId, string teamId);
        Task<TeamUserJoinRequest[]> GetAllUserJoinRequestsByTeam(CancellationToken ct, string teamId);
    }
}