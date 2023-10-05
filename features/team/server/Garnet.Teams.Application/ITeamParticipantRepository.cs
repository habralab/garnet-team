namespace Garnet.Teams.Application
{
    public interface ITeamParticipantRepository
    {
        Task<TeamParticipant> CreateTeamParticipant(CancellationToken ct, string userId, string username, string teamId);
        Task<TeamParticipant[]> GetParticipantsFromTeam(CancellationToken ct, string teamId);
        Task<TeamParticipant[]> DeleteTeamParticipants(CancellationToken ct, string teamId);
        Task<TeamParticipant[]> GetMembershipOfUser(CancellationToken ct, string userId);
        Task<TeamParticipant[]> FilterTeamParticipants(CancellationToken ct, TeamParticipantFilterParams filter);
        Task UpdateTeamParticipantUsername(CancellationToken ct, string userId, string username);
    }
}