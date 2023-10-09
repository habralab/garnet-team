namespace Garnet.Teams.Application
{
    public interface ITeamUserRepository
    {
        Task<TeamUser> AddUser(CancellationToken ct, string userId, string username);
        Task<TeamUser?> GetUser(CancellationToken ct, string userId);
        Task<TeamUser?> UpdateUser(CancellationToken ct, string userId, TeamUserUpdateArgs update);
        Task CreateIndexes(CancellationToken ct);
    }
}