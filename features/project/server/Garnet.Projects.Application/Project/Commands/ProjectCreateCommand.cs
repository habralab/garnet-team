using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Args;
using Garnet.Projects.Application.Project.Errors;
using Garnet.Projects.Application.ProjectUser;

namespace Garnet.Projects.Application.Project.Commands;

public class ProjectCreateCommand
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectUserRepository _projectUserRepository;
    private readonly IMessageBus _messageBus;

    public ProjectCreateCommand(
        ICurrentUserProvider currentUserProvider,
        IProjectRepository projectRepository,
        IProjectUserRepository projectUserRepository,
        IMessageBus messageBus
    )
    {
        _currentUserProvider = currentUserProvider;
        _projectRepository = projectRepository;
        _projectUserRepository = projectUserRepository;
        _messageBus = messageBus;
    }

    public async Task<Result<ProjectEntity>> Execute(CancellationToken ct, ProjectCreateArgs args)
    {
        var currentUserId = _currentUserProvider.UserId;
        
        var user = await _projectUserRepository.GetUser(ct, currentUserId);
        if (user is null)
        {
            return Result.Fail(new ProjectUserNotFoundError(currentUserId));
        }

        var project = await _projectRepository.CreateProject(ct, currentUserId, args);
        await _messageBus.Publish(project.ToCreatedEvent());
        return Result.Ok(project);
    }
}