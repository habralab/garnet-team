namespace Garnet.Teams.Application
{
    public interface ITeamJoinInvitationRepository
    {
        Task<TeamJoinInvitationEntity> CreateInvitation(CancellationToken ct, string userId, string teamId);
        Task<TeamJoinInvitationEntity[]> FilterInvitations(CancellationToken ct, TeamJoinInvitationFilterArgs filter);
    }
}