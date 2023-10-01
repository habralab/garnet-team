namespace Garnet.Teams.Application
{
    public interface ITeamRepository
    {
        Task<Team> CreateTeam(CancellationToken ct, string name, string description, string ownerUserId, string[] tags);
        Task<Team?> GetTeamById(CancellationToken ct, string teamId);
        Task<Team[]> FilterTeams(CancellationToken ct, string? search, string[] tags, int skip, int take);
        Task<Team?> DeleteTeam(CancellationToken ct, string teamId);
        Task<Team?> EditTeamDescription(CancellationToken ct, string teamId, string description);
        Task CreateIndexes(CancellationToken ct);
    }
}