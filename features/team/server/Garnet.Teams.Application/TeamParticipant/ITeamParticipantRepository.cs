using Garnet.Teams.Application.TeamParticipant.Args;

namespace Garnet.Teams.Application.TeamParticipant
{
    public interface ITeamParticipantRepository
    {
        Task<TeamParticipantEntity> CreateTeamParticipant(CancellationToken ct, TeamParticipantCreateArgs args);
        Task<TeamParticipantEntity[]> GetParticipantsFromTeam(CancellationToken ct, string teamId);
        Task DeleteParticipantsByTeam(CancellationToken ct, string teamId);
        Task DeleteParticipantById(CancellationToken ct, string participantId);
        Task<TeamParticipantEntity[]> GetMembershipOfUser(CancellationToken ct, string userId);
        Task<TeamParticipantEntity?> IsParticipantInTeam(CancellationToken ct, string userId, string teamId);
        Task<TeamParticipantEntity[]> FilterTeamParticipants(CancellationToken ct, TeamParticipantFilterArgs filter);
        Task<TeamParticipantEntity[]> TeamParticipantListOfTeams(CancellationToken ct, string[] teamIds);
        Task UpdateTeamParticipant(CancellationToken ct, string userId, TeamParticipantUpdateArgs update);
        Task CreateIndexes(CancellationToken ct);
    }
}