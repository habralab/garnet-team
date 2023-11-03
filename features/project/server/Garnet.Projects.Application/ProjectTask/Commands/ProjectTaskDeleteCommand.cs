using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.Project.Errors;
using Garnet.Projects.Application.ProjectTask.Errors;
using Garnet.Projects.Application.ProjectTeamParticipant;

namespace Garnet.Projects.Application.ProjectTask.Commands;

public class ProjectTaskDeleteCommand
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectTeamParticipantRepository _projectTeamParticipantRepository;
    private readonly IProjectTaskRepository _projectTaskRepository;
    private readonly IMessageBus _messageBus;

    public ProjectTaskDeleteCommand(
        ICurrentUserProvider currentUserProvider,
        IProjectRepository projectRepository,
        IProjectTeamParticipantRepository projectTeamParticipantRepository,
        IMessageBus messageBus, IProjectTaskRepository projectTaskRepository)
    {
        _currentUserProvider = currentUserProvider;
        _projectRepository = projectRepository;
        _projectTeamParticipantRepository = projectTeamParticipantRepository;
        _messageBus = messageBus;
        _projectTaskRepository = projectTaskRepository;
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

        var teamParticipants =
            await _projectTeamParticipantRepository.GetProjectTeamParticipantsByProjectId(ct, project.Id);
        var user = teamParticipants.FirstOrDefault(x => x.UserParticipants.Any(
            o => o.Id == currentUserId));
        if (user is null)
        {
            return Result.Fail(new ProjectOnlyParticipantCanDeleteTaskError());
        }

        task = await _projectTaskRepository.DeleteProjectTask(ct, taskId);
        await _messageBus.Publish(task.ToDeletedEvent());
        return Result.Ok(task);
    }
}