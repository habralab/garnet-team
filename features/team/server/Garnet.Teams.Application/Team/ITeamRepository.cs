namespace Garnet.Teams.Application
{
    public interface ITeamRepository
    {
        Task<TeamEntity> CreateTeam(CancellationToken ct, string name, string description, string ownerUserId, string[] tags);
        Task<TeamEntity?> GetTeamById(CancellationToken ct, string teamId);
        Task<TeamEntity[]> FilterTeams(CancellationToken ct, string? search, string[] tags, int skip, int take);
        Task<TeamEntity?> DeleteTeam(CancellationToken ct, string teamId);
        Task<TeamEntity?> EditTeamDescription(CancellationToken ct, string teamId, string description);
        Task<TeamEntity?> EditTeamOwner(CancellationToken ct, string teamId, string newOwnerUserId);
        Task CreateIndexes(CancellationToken ct);
    }
}