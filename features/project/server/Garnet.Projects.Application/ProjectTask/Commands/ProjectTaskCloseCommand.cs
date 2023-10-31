using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.Project.Errors;
using Garnet.Projects.Application.ProjectTask.Args;
using Garnet.Projects.Application.ProjectTask.Errors;

namespace Garnet.Projects.Application.ProjectTask.Commands;

public class ProjectTaskCloseCommand
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IProjectTaskRepository _projectTaskRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMessageBus _messageBus;

    public ProjectTaskCloseCommand(
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

    public async Task<Result<ProjectTaskEntity>> Execute(CancellationToken ct, string taskId)
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

        if (currentUserId != task.ResponsibleUserId && currentUserId != project.OwnerUserId)
        {
            return Result.Fail(new ProjectTaskResponsiblePersonOnlyCanCloseTaskError());
        }

        if (task.Status == ProjectTaskStatuses.Close)
        {
            return Result.Fail(new ProjectTaskAlreadySetThisStatusError(task.Status));
        }

        task = await _projectTaskRepository.CloseProjectTask(ct, taskId, ProjectTaskStatuses.Close);

        await _messageBus.Publish(task.ToClosedEvent());
        return Result.Ok(task);
    }
}