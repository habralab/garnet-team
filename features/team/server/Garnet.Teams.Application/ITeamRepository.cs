namespace Garnet.Teams.Application
{
    public interface ITeamRepository
    {
        Task<Team> CreateTeam(CancellationToken ct, string name, string description, string ownerUserId, string[] tags);
        Task<Team?> GetTeamById(CancellationToken ct, string teamId);
        Task CreateIndexes(CancellationToken ct);
    }
}