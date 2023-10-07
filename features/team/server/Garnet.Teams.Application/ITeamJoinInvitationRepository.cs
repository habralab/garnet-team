namespace Garnet.Teams.Application
{
    public interface ITeamJoinInvitationRepository
    {
        Task<TeamJoinInvitation> CreateInvitation(CancellationToken ct, string userId, string teamId);
        Task<TeamJoinInvitation> FilterInvitations(CancellationToken ct, TeamJoinInvitationFilterArgs filter);
    }
}