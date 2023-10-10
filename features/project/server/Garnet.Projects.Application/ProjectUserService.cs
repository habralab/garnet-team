using Garnet.Projects.Application.ProjectUser;

namespace Garnet.Projects.Application;

public class ProjectUserService
{
    private readonly IProjectUserRepository _repository;

    public ProjectUserService(IProjectUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProjectUserEntity> AddUser(CancellationToken ct, string userId)
    {
        return await _repository.AddUser(ct, userId);
    }
    public async Task<ProjectUserEntity?> GetUser(CancellationToken ct, string userId)
    {
        return await _repository.GetUser(ct, userId);
    }
}