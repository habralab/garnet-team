﻿using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Project.Errors;
using Garnet.Projects.Application.ProjectTeamParticipant;

namespace Garnet.Projects.Application.Project.Commands;

public class ProjectDeleteCommand
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectTeamParticipantRepository _projectTeamParticipantRepository;
    private readonly IMessageBus _messageBus;

    public ProjectDeleteCommand(
        ICurrentUserProvider currentUserProvider,
        IProjectRepository projectRepository,
        IProjectTeamParticipantRepository projectTeamParticipantRepository,
        IMessageBus messageBus
    )
    {
        _currentUserProvider = currentUserProvider;
        _projectRepository = projectRepository;
        _projectTeamParticipantRepository = projectTeamParticipantRepository;
        _messageBus = messageBus;
    }

    public async Task<Result<ProjectEntity>> Execute(CancellationToken ct, string projectId)
    {
        var project = await _projectRepository.GetProject(ct, projectId);
        if (project is null)
        {
            return Result.Fail(new ProjectNotFoundError(projectId));
        }

        if (project.OwnerUserId != _currentUserProvider.UserId)
        {
            return Result.Fail(new ProjectOnlyOwnerCanDeleteError());
        }

        var teamParticipants =
            await _projectTeamParticipantRepository.GetProjectTeamParticipantsByProjectId(ct, projectId);
        var teamParticipantIds = teamParticipants.Select(x => x.TeamId).Distinct().ToArray();
        await _projectRepository.DeleteProject(ct, projectId);
        await _projectTeamParticipantRepository.DeleteProjectTeamParticipantsByProjectId(ct, projectId);
        await _messageBus.Publish(project.ToDeletedEvent(teamParticipantIds));
        return Result.Ok(project);
    }
}