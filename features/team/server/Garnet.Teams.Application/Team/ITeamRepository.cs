using Garnet.Teams.Application.Team.Args;

namespace Garnet.Teams.Application.Team
{
    public interface ITeamRepository
    {
        Task<TeamEntity> CreateTeam(CancellationToken ct, string ownerUserId, TeamCreateArgs args);
        Task<TeamEntity?> GetTeamById(CancellationToken ct, string teamId);
        Task<TeamEntity[]> FilterTeams(CancellationToken ct, TeamFilterArgs args);
        Task<TeamEntity?> DeleteTeam(CancellationToken ct, string teamId);
        Task<TeamEntity?> EditTeamDescription(CancellationToken ct, string teamId, string description);
        Task<TeamEntity?> EditTeamName(CancellationToken ct, string teamId, string name);
        Task<TeamEntity?> EditTeamTags(CancellationToken ct, string teamId, string[] tags);
        Task<TeamEntity?> EditTeamAvatar(CancellationToken ct, string teamId, string avatarUrl);
        Task<TeamEntity?> EditTeamOwner(CancellationToken ct, string teamId, string newOwnerUserId);
        Task<TeamEntity[]> GetTeamsById(CancellationToken ct, string[] teamIds, TeamsListArgs args);
        Task CreateIndexes(CancellationToken ct);
        Task<TeamEntity?> AddProjectId(CancellationToken ct, string teamId, string projectId);
        Task<TeamEntity?> RemoveProjectId(CancellationToken ct, string teamId, string projectId);
        Task RemoveProjectInAllTeams(CancellationToken ct, string projectId);
        Task<TeamEntity?> IncreaseParticipantCount(CancellationToken ct, string teamId, string? participantAvatarUrl);
        Task<TeamEntity?> DecreaseParticipantCount(CancellationToken ct, string teamId, string? participantAvatarUrl);
        Task<TeamEntity?> UpdateParticipantAvatarUrl(CancellationToken ct, string[] teamIds, string? oldParticipantAvatarUrl, string? newParticipantAvatarUrl);
    }
}