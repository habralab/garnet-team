﻿using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.ProjectTask.Args;
using Garnet.Projects.Application.ProjectTask.Errors;
using Garnet.Projects.Application.ProjectTeamParticipant;

namespace Garnet.Projects.Application.ProjectTask.Commands;

public class ProjectTaskCreateCommand
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IProjectTaskRepository _projectTaskRepository;
    private readonly IProjectTeamParticipantRepository _projectTeamParticipantRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMessageBus _messageBus;

    public ProjectTaskCreateCommand(
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

    public async Task<Result<ProjectTaskEntity>> Execute(CancellationToken ct, ProjectTaskCreateArgs args)
    {
        var currentUserId = _currentUserProvider.UserId;

        args = args with { Name = args.Name.Trim() };
        if (string.IsNullOrWhiteSpace(args.Name))
        {
            return Result.Fail(new ProjectTaskNameCanNotBeEmptyError());
        }

        var teamParticipants =
            await _projectTeamParticipantRepository.GetProjectTeamParticipantsByProjectId(ct, args.ProjectId);
        var user = teamParticipants.FirstOrDefault(x => x.UserParticipants.Any(
            o => o.Id == currentUserId));
        if (user is null)
        {
            return Result.Fail(new ProjectOnlyParticipantCanCreateTaskError());
        }

        var teamParticipantIds = teamParticipants.Select(x => x.TeamId).ToArray();
        if (args.TeamExecutorIds.Any(team => teamParticipantIds.Contains(team) is false))
        {
            return Result.Fail(new ProjectTeamParticipantOnlyCanBeSetAsTeamExecutorError());
        }

        var userParticipants =
            teamParticipants.SelectMany(x => x.UserParticipants).Select(u => u.Id).Distinct().ToArray();
        if (args.UserExecutorIds.Any(userId => userParticipants.Contains(userId) is false))
        {
            return Result.Fail(new ProjectOnlyParticipantCanSetAsTaskExecutorError());
        }


        var taskNumber = await _projectRepository.GetProjectTasksCounter(ct, args.ProjectId);

        var status = ProjectTaskStatuses.Open;
        var task = await _projectTaskRepository.CreateProjectTask(ct, currentUserId, status, taskNumber, args);
        await _projectRepository.IncrementProjectTasksCounter(ct, args.ProjectId);

        await _messageBus.Publish(task.ToCreatedEvent());
        return Result.Ok(task);
    }
}