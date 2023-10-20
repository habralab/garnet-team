namespace Garnet.Projects.Application.ProjectUser;

public interface IProjectUserRepository
{
    Task<ProjectUserEntity> AddUser(CancellationToken ct, string userId, string userName);
    Task<ProjectUserEntity?> GetUser(CancellationToken ct, string userId);
}