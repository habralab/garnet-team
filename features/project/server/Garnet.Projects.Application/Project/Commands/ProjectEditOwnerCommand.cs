using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Project.Errors;
using Garnet.Projects.Application.ProjectUser;

namespace Garnet.Projects.Application.Project.Commands;

public class ProjectEditOwnerCommand
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectUserRepository _projectUserRepository;
    private readonly IMessageBus _messageBus;

    public ProjectEditOwnerCommand(
        ICurrentUserProvider currentUserProvider,
        IProjectRepository projectRepository,
        IProjectUserRepository projectUserRepository,
        IMessageBus messageBus)
    {
        _currentUserProvider = currentUserProvider;
        _projectRepository = projectRepository;
        _projectUserRepository = projectUserRepository;
        _messageBus = messageBus;
    }

    public async Task<Result<ProjectEntity>> Execute(CancellationToken ct,
        string projectId, string newOwnerUserId)
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

        var user = await _projectUserRepository.GetUser(ct, newOwnerUserId);
        if (user is null)
        {
            return Result.Fail(new ProjectUserNotFoundError(newOwnerUserId));
        }

        project = await _projectRepository.EditProjectOwner(ct, projectId, newOwnerUserId);

        await _messageBus.Publish(project.ToUpdatedEvent());
        return Result.Ok(project);
    }
}