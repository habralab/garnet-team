using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.Project.Errors;
using Garnet.Projects.Application.ProjectTask.Errors;
using Garnet.Projects.Application.ProjectTeamParticipant;

namespace Garnet.Projects.Application.ProjectTask.Commands;

public class ProjectTaskEditDescriptionCommand
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IProjectTaskRepository _projectTaskRepository;
    private readonly IProjectTeamParticipantRepository _projectTeamParticipantRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMessageBus _messageBus;

    public ProjectTaskEditDescriptionCommand(
        ICurrentUserProvider currentUserProvider,
        IProjectTaskRepository projectTaskRepository,
        IProjectTeamParticipantRepository projectTeamParticipantRepository,
        IMessageBus messageBus, IProjectRepository projectRepository)
    {
        _currentUserProvider = currentUserProvider;
        _projectTaskRepository = projectTaskRepository;
        _projectTeamParticipantRepository = projectTeamParticipantRepository;
        _messageBus = messageBus;
        _projectRepository = projectRepository;
    }

    public async Task<Result<ProjectTaskEntity>> Execute(CancellationToken ct, string taskId, string description)
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

        var teamParticipants =
            await _projectTeamParticipantRepository.GetProjectTeamParticipantsByProjectId(ct, task.ProjectId);
        var user = teamParticipants.FirstOrDefault(x => x.UserParticipants.Any(
            o => o.Id == currentUserId));
        if (user is null)
        {
            return Result.Fail(new ProjectOnlyParticipantCanEditTaskError());
        }


        task = await _projectTaskRepository.EditProjectTaskDescription(ct, taskId, description);

        await _messageBus.Publish(task.ToUpdatedEvent());
        return Result.Ok(task);
    }
}