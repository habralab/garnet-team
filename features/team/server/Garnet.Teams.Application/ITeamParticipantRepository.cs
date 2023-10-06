namespace Garnet.Teams.Application
{
    public interface ITeamParticipantRepository
    {
        Task<TeamParticipant> CreateTeamParticipant(CancellationToken ct, string userId, string username, string teamId);
        Task<TeamParticipant[]> GetParticipantsFromTeam(CancellationToken ct, string teamId);
        Task DeleteParticipantsByTeam(CancellationToken ct, string teamId);
        Task<TeamParticipant[]> GetMembershipOfUser(CancellationToken ct, string userId);
        Task<TeamParticipant[]> FilterTeamParticipants(CancellationToken ct, TeamUserFilterArgs filter);
        Task UpdateTeamParticipant(CancellationToken ct, string userId, TeamParticipantUpdateArgs update);
    }
}