namespace Garnet.Projects.Application.ProjectUser;

public interface IProjectUserRepository
{
    Task<ProjectUserEntity> AddUser(CancellationToken ct, string userId, string userName);
    Task UpdateUser(CancellationToken ct, string userId, string userName, string userAvatarUrl);
    Task<ProjectUserEntity?> GetUser(CancellationToken ct, string userId);
}