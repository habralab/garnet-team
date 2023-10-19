using Garnet.Teams.Application.TeamJoinInvitation.Args;

namespace Garnet.Teams.Application.TeamJoinInvitation
{
    public interface ITeamJoinInvitationRepository
    {
        Task<TeamJoinInvitationEntity> CreateInvitation(CancellationToken ct, string userId, string teamId);
        Task<TeamJoinInvitationEntity?> GetById(CancellationToken ct, string joinInvitationId);
        Task<TeamJoinInvitationEntity[]> FilterInvitations(CancellationToken ct, TeamJoinInvitationFilterArgs filter);
        Task DeleteInvitationsByTeam(CancellationToken ct, string teamId);
        Task<TeamJoinInvitationEntity?> DeleteInvitationsById(CancellationToken ct, string joinInvitationId);
    }
}