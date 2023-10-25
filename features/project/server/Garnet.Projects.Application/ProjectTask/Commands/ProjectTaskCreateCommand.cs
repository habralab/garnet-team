using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.ProjectTask.Args;
using Garnet.Projects.Application.ProjectTask.Errors;
using Garnet.Projects.Application.ProjectTeamParticipant;

namespace Garnet.Projects.Application.ProjectTask.Commands;

public class ProjectTaskCreateCommand
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IProjectTaskRepository _projectTaskRepository;
    private readonly IProjectTeamParticipantRepository _projectTeamParticipantRepository;
    private readonly IMessageBus _messageBus;

    public ProjectTaskCreateCommand(
        ICurrentUserProvider currentUserProvider,
        IProjectTaskRepository projectTaskRepository,
        IProjectTeamParticipantRepository projectTeamParticipantRepository,
        IMessageBus messageBus)
    {
        _currentUserProvider = currentUserProvider;
        _projectTaskRepository = projectTaskRepository;
        _projectTeamParticipantRepository = projectTeamParticipantRepository;
        _messageBus = messageBus;
    }

    public async Task<Result<ProjectTaskEntity>> Execute(CancellationToken ct, ProjectTaskCreateArgs args)
    {
        var currentUserId = _currentUserProvider.UserId;

        var teamParticipants =
            await _projectTeamParticipantRepository.GetProjectTeamParticipantsByProjectId(ct, args.ProjectId);
        var user = teamParticipants.Select(x => x.UserParticipants.Where(u => u.Id == currentUserId)).FirstOrDefault();

        if (user is null)
        {
            return Result.Fail(new ProjectOnlyParticipantCanCreateTaskError());
        }

        args = args with { Name = args.Name.Trim() };
        if (string.IsNullOrWhiteSpace(args.Name))
        {
            return Result.Fail(new ProjectTaskNameCanNotBeEmptyError());
        }

        var task = await _projectTaskRepository.CreateProjectTask(ct, currentUserId, args);


        await _messageBus.Publish(task.ToCreatedEvent());
        return Result.Ok(task);
    }
}