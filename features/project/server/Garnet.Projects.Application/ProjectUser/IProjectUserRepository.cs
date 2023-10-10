namespace Garnet.Projects.Application;

public interface IProjectUserRepository
{
    Task<ProjectUser> AddUser(CancellationToken ct, string userId);
    Task<ProjectUser?> GetUser(CancellationToken ct, string userId);
}