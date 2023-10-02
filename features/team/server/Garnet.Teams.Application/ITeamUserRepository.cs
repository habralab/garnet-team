namespace Garnet.Teams.Application
{
    public interface ITeamUserRepository
    {
        Task<TeamUser> AddUser(CancellationToken ct, string userId);
        Task<TeamUser?> GetUser(CancellationToken ct, string userId);
    }
}