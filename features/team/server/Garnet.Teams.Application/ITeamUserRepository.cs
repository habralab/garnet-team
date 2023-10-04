namespace Garnet.Teams.Application
{
    public interface ITeamUserRepository
    {
        Task<TeamUser> AddUser(CancellationToken ct, string userId, string username);
        Task<TeamUser?> GetUser(CancellationToken ct, string userId);
        Task<TeamUser[]> FindUser(CancellationToken ct, string query);
        Task CreateIndexes(CancellationToken ct);
    }
}