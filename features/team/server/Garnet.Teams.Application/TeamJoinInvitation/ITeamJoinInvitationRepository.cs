using Garnet.Teams.Application.TeamJoinInvitation.Args;

namespace Garnet.Teams.Application.TeamJoinInvitation
{
    public interface ITeamJoinInvitationRepository
    {
        Task<TeamJoinInvitationEntity> CreateInvitation(CancellationToken ct, string userId, string teamId);
        Task<TeamJoinInvitationEntity[]> FilterInvitations(CancellationToken ct, TeamJoinInvitationFilterArgs filter);
    }
}