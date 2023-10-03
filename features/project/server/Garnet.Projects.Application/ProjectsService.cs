using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Errors;

namespace Garnet.Projects.Application;

public class ProjectsService
{
    private readonly IProjectsRepository _repository;
    private readonly IMessageBus _messageBus;

    public ProjectsService(
        IProjectsRepository repository,
        IMessageBus messageBus
    )
    {
        _repository = repository;
        _messageBus = messageBus;
    }

    public async Task<Project> CreateProject(CancellationToken ct, ICurrentUserProvider currentUserProvider,
        string projectName, string? description)
    {
        var project = await _repository.CreateProject(ct, currentUserProvider.UserId, projectName, description);
        await _messageBus.Publish(project.ToCreatedEvent());
        return project;
    }

    public async Task<Project?> GetProject(CancellationToken ct, string projectId)
    {
        return await _repository.GetProject(ct, projectId);
    }

    public async Task<Result<Project>> EditProjectDescription(CancellationToken ct,
        ICurrentUserProvider currentUserProvider,
        string projectId, string? description)
    {
        var project = await GetProject(ct, projectId);

        if (project is null)
        {
            return Result.Fail($"Проект с идентификатором '{projectId}' не найден");
        }

        if (project.OwnerUserId != currentUserProvider.UserId)
        {
            return Result.Fail("Проект может отредактировать только его владелец");
        }

        project = await _repository.EditProjectDescription(ct, projectId, description);

        await _messageBus.Publish(project.ToUpdatedEvent());
        return Result.Ok(project);
    }

    public async Task<Result<Project>> DeleteProject(CancellationToken ct, ICurrentUserProvider currentUserProvider,
        string projectId)
    {
        var project = await _repository.GetProject(ct, projectId);

        if (project is null)
        {
            return Result.Fail(new ProjectNotFoundError(projectId));
        }

        if (project.OwnerUserId != currentUserProvider.UserId)
        {
            return Result.Fail(new ProjectOnlyOwnerCanDeleteError());
        }

        await _repository.DeleteProject(ct, projectId);
        await _messageBus.Publish(project.EntityDeletedEvent());
        return Result.Ok(project);
    }
}