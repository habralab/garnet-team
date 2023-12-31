
namespace Garnet.Teams.Application.TeamUserJoinRequest
{
    public interface ITeamUserJoinRequestRepository
    {
        Task<TeamUserJoinRequestEntity> CreateJoinRequestByUser(CancellationToken ct, string userId, string teamId);
        Task<TeamUserJoinRequestEntity[]> GetAllUserJoinRequestsByTeam(CancellationToken ct, string teamId);
        Task<TeamUserJoinRequestEntity?> GetUserJoinRequestById(CancellationToken ct, string userJoinRequestId);
        Task<TeamUserJoinRequestEntity?> DeleteUserJoinRequestById(CancellationToken ct, string userJoinRequestId);
        Task DeleteUserJoinRequestsByTeam(CancellationToken ct, string teamId);
    }
}