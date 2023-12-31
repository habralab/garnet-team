
namespace Garnet.Teams.Application.TeamJoinProjectRequest
{
    public interface ITeamJoinProjectRequestRepository
    {
        Task<TeamJoinProjectRequestEntity> CreateJoinProjectRequest(CancellationToken ct, string teamId, string projectId);
        Task<TeamJoinProjectRequestEntity[]> GetJoinProjectRequestsByTeam(CancellationToken ct, string teamId);
        Task<TeamJoinProjectRequestEntity?> DeleteJoinProjectRequestById(CancellationToken ct, string joinProjectRequestId);
        Task DeleteJoinProjectRequestByProject(CancellationToken ct, string projectId);
        Task DeleteJoinProjectRequestByTeam(CancellationToken ct, string teamId);
    }
}