using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.Project.Errors;
using Garnet.Projects.Application.ProjectTask.Errors;

namespace Garnet.Projects.Application.ProjectTask.Commands;

public class ProjectTaskEditResponsibleUserCommand
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IProjectTaskRepository _projectTaskRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMessageBus _messageBus;

    public ProjectTaskEditResponsibleUserCommand(
        ICurrentUserProvider currentUserProvider,
        IProjectTaskRepository projectTaskRepository,
        IMessageBus messageBus,
        IProjectRepository projectRepository)
    {
        _currentUserProvider = currentUserProvider;
        _projectTaskRepository = projectTaskRepository;
        _messageBus = messageBus;
        _projectRepository = projectRepository;
    }

    public async Task<Result<ProjectTaskEntity>> Execute(CancellationToken ct, string taskId, string newResponsibleUserId)
    {
        var currentUserId = _currentUserProvider.UserId;

        var task = await _projectTaskRepository.GetProjectTaskById(ct, taskId);
        if (task is null)
        {
            return Result.Fail(new ProjectTaskNotFoundError(taskId));
        }

        var project = await _projectRepository.GetProject(ct, task.ProjectId);
        if (project is null)
        {
            return Result.Fail(new ProjectNotFoundError(task.ProjectId));
        }

        if (newResponsibleUserId == task.ResponsibleUserId)
        {
            return Result.Fail(new ProjectTaskThisUserIsAlreadySetTaskResponsibleUserError());
        }

        if (currentUserId != task.ResponsibleUserId && currentUserId != project.OwnerUserId)
        {
            return Result.Fail(new ProjectTaskResponsiblePersonOnlyCanCloseTaskError());
        }

        task = await _projectTaskRepository.EditProjectTaskResponsibleUser(ct, taskId, newResponsibleUserId);

        await _messageBus.Publish(task.ToUpdatedEvent());
        return Result.Ok(task);
    }
}