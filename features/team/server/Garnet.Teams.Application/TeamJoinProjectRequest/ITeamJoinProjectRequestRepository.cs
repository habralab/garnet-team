namespace Garnet.Teams.Application
{
    public interface ITeamJoinProjectRequestRepository
    {
        Task<TeamJoinProjectRequest> CreateJoinProjectRequest(CancellationToken ct, string teamId, string projectId);
        Task<TeamJoinProjectRequest[]> GetJoinProjectRequestsByTeam(CancellationToken ct, string teamId);
        Task<TeamJoinProjectRequest?> DeleteJoinProjectRequestById(CancellationToken ct, string joinProjectRequestId);
        Task DeleteJoinProjectRequestByProject(CancellationToken ct, string projectId);
    }
}