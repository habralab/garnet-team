namespace Garnet.Teams.Application
{
    public interface ITeamMembershipRepository
    {
        Task<TeamUserJoinRequest> CreateJoinRequestByUser(CancellationToken ct, string userId, string teamId);
        Task<TeamUserJoinRequest[]> GetAllUserJoinRequestByTeam(CancellationToken ct, string teamId);
    }
}