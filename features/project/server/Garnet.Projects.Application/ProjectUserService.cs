using FluentResults;
using Garnet.Projects.Application.Errors;

namespace Garnet.Projects.Application;

public class ProjectUserService
{
    private readonly IProjectUserRepository _repository;

    public ProjectUserService(IProjectUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProjectUser> AddUser(CancellationToken ct, string userId)
    {
        return await _repository.AddUser(ct, userId);
    }
    public async Task<ProjectUser?> GetUser(CancellationToken ct, string userId)
    {
        return await _repository.GetUser(ct, userId);
    }
}