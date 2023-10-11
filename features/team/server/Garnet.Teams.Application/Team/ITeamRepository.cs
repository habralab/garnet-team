using Garnet.Teams.Application.Team.Args;

namespace Garnet.Teams.Application.Team
{
    public interface ITeamRepository
    {
        Task<TeamEntity> CreateTeam(CancellationToken ct, TeamCreateArgs args);
        Task<TeamEntity?> GetTeamById(CancellationToken ct, string teamId);
        Task<TeamEntity[]> FilterTeams(CancellationToken ct, TeamFilterArgs args);
        Task<TeamEntity?> DeleteTeam(CancellationToken ct, string teamId);
        Task<TeamEntity?> EditTeamDescription(CancellationToken ct, string teamId, string description);
        Task<TeamEntity?> EditTeamAvatar(CancellationToken ct, string teamId, string avatarUrl);
        Task<TeamEntity?> EditTeamOwner(CancellationToken ct, string teamId, string newOwnerUserId);
        Task CreateIndexes(CancellationToken ct);
    }
}