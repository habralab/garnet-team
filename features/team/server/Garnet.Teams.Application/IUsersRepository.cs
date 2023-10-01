namespace Garnet.Teams.Application
{
    public interface IUserRepository
    {
        Task<string> AddUser(CancellationToken ct, string userId);
        Task<string?> GetUser(CancellationToken ct, string userId);
    }
}