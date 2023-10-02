using FluentResults;
using Garnet.Common.Application;

namespace Garnet.Projects.Application;

public class ProjectsService
{
    private readonly IProjectsRepository _repository;

    public ProjectsService(
        IProjectsRepository repository
    )
    {
        _repository = repository;
    }

    public async Task<Project> CreateProject(CancellationToken ct, ICurrentUserProvider currentUserProvider,
        string projectName, string? description)
    {
        return await _repository.CreateProject(ct, currentUserProvider.UserId, projectName, description);
    }

    public async Task<Project?> GetProject(CancellationToken ct, string projectId)
    {
        return await _repository.GetProject(ct, projectId);
    }

    public async Task<Result<Project>> DeleteProject(CancellationToken ct, ICurrentUserProvider currentUserProvider,
        string projectId)
    {
        var project = await _repository.GetProject(ct, projectId);

        if (project is null)
        {
            return Result.Fail($"Проект с идентификатором '{projectId}' не найден");
        }

        if (project.OwnerUserId != currentUserProvider.UserId)
        {
            return Result.Fail("Проект может удалить только его владелец");
        }

        await _repository.DeleteProject(ct, projectId);

        return Result.Ok(project);
    }
}