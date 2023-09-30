namespace Garnet.Teams.Application
{
    public interface ITeamParticipantRepository
    {
        Task<TeamParticipant> CreateTeamParticipant(CancellationToken ct, string userId, string teamId);
        Task<TeamParticipant[]> GetParticipantsFromTeam(CancellationToken ct, string teamId);
        Task<TeamParticipant[]> DeleteTeamParticipants(CancellationToken ct, string teamId);
    }
}