using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.Project.Errors;
using Garnet.Projects.Application.ProjectTask.Errors;
using Garnet.Projects.Application.ProjectTeamParticipant;

namespace Garnet.Projects.Application.ProjectTask.Commands;

public class ProjectTaskEditNameCommand
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IProjectTaskRepository _projectTaskRepository;
    private readonly IProjectTeamParticipantRepository _projectTeamParticipantRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMessageBus _messageBus;

    public ProjectTaskEditNameCommand(
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

    public async Task<Result<ProjectTaskEntity>> Execute(CancellationToken ct,
        string projectId, string taskName, string newTaskName)
    {
        var currentUserId = _currentUserProvider.UserId;

        if (string.IsNullOrEmpty(newTaskName))
        {
            return Result.Fail(new ProjectTaskNameCanNotBeEmptyError());
        }

        var project = await _projectRepository.GetProject(ct, projectId);
        if (project is null)
        {
            return Result.Fail(new ProjectNotFoundError(projectId));
        }

        var teamParticipants =
            await _projectTeamParticipantRepository.GetProjectTeamParticipantsByProjectId(ct, projectId);
        var user = teamParticipants.FirstOrDefault(x => x.UserParticipants.Any(
            o => o.Id == currentUserId));
        if (user is null)
        {
            return Result.Fail(new ProjectOnlyParticipantCanEditTaskError());
        }

        var task = await _projectTaskRepository.EditProjectTaskName(ct, projectId, taskName, newTaskName);

        await _messageBus.Publish(task.ToUpdatedEvent());
        return Result.Ok(task);
    }
}