using Garnet.Teams.Application.TeamParticipant.Args;

namespace Garnet.Teams.Application.TeamParticipant
{
    public interface ITeamParticipantRepository
    {
        Task<TeamParticipantEntity> CreateTeamParticipant(CancellationToken ct, string userId, string username, string teamId);
        Task<TeamParticipantEntity[]> GetParticipantsFromTeam(CancellationToken ct, string teamId);
        Task<TeamParticipantEntity[]> DeleteTeamParticipants(CancellationToken ct, string teamId);
        Task<TeamParticipantEntity[]> GetMembershipOfUser(CancellationToken ct, string userId);
        Task<TeamParticipantEntity[]> FilterTeamParticipants(CancellationToken ct, TeamParticipantFilterArgs filter);
        Task UpdateTeamParticipant(CancellationToken ct, string userId, TeamParticipantUpdateArgs update);
    }
}