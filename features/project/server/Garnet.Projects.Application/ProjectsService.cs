using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Errors;

namespace Garnet.Projects.Application;

public class ProjectsService
{
    private readonly IProjectsRepository _repository;
    private readonly IMessageBus _messageBus;
    private readonly ProjectUserService _projectUserService;

    public ProjectsService(
        IProjectsRepository repository,
        IMessageBus messageBus,
        ProjectUserService projectUserService
    )
    {
        _repository = repository;
        _messageBus = messageBus;
        _projectUserService = projectUserService;
    }

    public async Task<Project> CreateProject(CancellationToken ct, ICurrentUserProvider currentUserProvider,
        string projectName, string? description)
    {
        var project = await _repository.CreateProject(ct, currentUserProvider.UserId, projectName, description);
        await _messageBus.Publish(project.ToCreatedEvent());
        return project;
    }

    public async Task<Result<Project>> GetProject(CancellationToken ct, string projectId)
    {
        var project = await _repository.GetProject(ct, projectId);
        return project is null ? Result.Fail(new ProjectNotFoundError(projectId)) : Result.Ok(project);
    }

    public async Task<Result<Project>> EditProjectDescription(CancellationToken ct,
        ICurrentUserProvider currentUserProvider,
        string projectId, string? description)
    {
        var result = await GetProject(ct, projectId);

        if (result.IsFailed)
        {
            return Result.Fail(result.Errors);
        }

        var project = result.Value;
        if (project.OwnerUserId != currentUserProvider.UserId)
        {
            return Result.Fail(new ProjectOnlyOwnerCanEditError());
        }

        project = await _repository.EditProjectDescription(ct, projectId, description);

        await _messageBus.Publish(project.ToUpdatedEvent());
        return Result.Ok(project);
    }

    public async Task<Result<Project>> DeleteProject(CancellationToken ct, ICurrentUserProvider currentUserProvider,
        string projectId)
    {
        var result = await GetProject(ct, projectId);

        if (result.IsFailed)
        {
            return Result.Fail(result.Errors);
        }

        var project = result.Value;
        if (project.OwnerUserId != currentUserProvider.UserId)
        {
            return Result.Fail(new ProjectOnlyOwnerCanDeleteError());
        }

        await _repository.DeleteProject(ct, projectId);
        await _messageBus.Publish(project.ToDeletedEvent());
        return Result.Ok(project);
    }

    public async Task<Result<Project>> EditProjectOwner(CancellationToken ct,
        ICurrentUserProvider currentUserProvider,
        string projectId, string newOwnerUserId)
    {
        var result = await GetProject(ct, projectId);

        if (result.IsFailed)
        {
            return Result.Fail(result.Errors);
        }

        var user = await _projectUserService.GetUser(ct, newOwnerUserId);
        if (user is null)
        {
            return Result.Fail(new ProjectUserNotFoundError(newOwnerUserId));
        }

        var project = result.Value;
        if (project.OwnerUserId != currentUserProvider.UserId)
        {
            return Result.Fail(new ProjectOnlyOwnerCanEditError());
        }

        project = await _repository.EditProjectOwner(ct, projectId, newOwnerUserId);

        await _messageBus.Publish(project.ToUpdatedEvent());
        return Result.Ok(project);
    }
}