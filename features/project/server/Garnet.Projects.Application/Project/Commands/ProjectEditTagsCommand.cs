﻿using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Project.Errors;

namespace Garnet.Projects.Application.Project.Commands;

public class ProjectEditTagsCommand
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IProjectRepository _projectRepository;
    private readonly IMessageBus _messageBus;

    public ProjectEditTagsCommand(
        ICurrentUserProvider currentUserProvider,
        IProjectRepository projectRepository,
        IMessageBus messageBus
    )
    {
        _currentUserProvider = currentUserProvider;
        _projectRepository = projectRepository;
        _messageBus = messageBus;
    }

    public async Task<Result<ProjectEntity>> Execute(CancellationToken ct,
        string projectId, string[] tags)
    {
        var project = await _projectRepository.GetProject(ct, projectId);

        if (project is null)
        {
            return Result.Fail(new ProjectNotFoundError(projectId));
        }

        if (project.OwnerUserId != _currentUserProvider.UserId)
        {
            return Result.Fail(new ProjectOnlyOwnerCanEditError());
        }

        project = await _projectRepository.EditProjectTags(ct, projectId, tags);

        await _messageBus.Publish(project.ToUpdatedEvent());
        return Result.Ok(project);
    }
}