using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Common.Application.S3;
using Garnet.Projects.Application.Project.Args;
using Garnet.Projects.Application.Project.Errors;
using Garnet.Projects.Application.ProjectUser;

namespace Garnet.Projects.Application.Project.Commands;

public class ProjectCreateCommand
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectUserRepository _projectUserRepository;
    private readonly IMessageBus _messageBus;
    private readonly IRemoteFileStorage _fileStorage;

    public ProjectCreateCommand(
        ICurrentUserProvider currentUserProvider,
        IProjectRepository projectRepository,
        IProjectUserRepository projectUserRepository,
        IMessageBus messageBus,
        IRemoteFileStorage fileStorage
    )
    {
        _currentUserProvider = currentUserProvider;
        _projectRepository = projectRepository;
        _projectUserRepository = projectUserRepository;
        _messageBus = messageBus;
        _fileStorage = fileStorage;
    }

    public async Task<Result<ProjectEntity>> Execute(CancellationToken ct, ProjectCreateArgs args)
    {
        var currentUserId = _currentUserProvider.UserId;

        var user = await _projectUserRepository.GetUser(ct, currentUserId);
        if (user is null)
        {
            return Result.Fail(new ProjectUserNotFoundError(currentUserId));
        }

        args = args with { ProjectName = args.ProjectName.Trim() };
        if (string.IsNullOrWhiteSpace(args.ProjectName))
        {
            return Result.Fail(new ProjectNameCanNotBeEmptyError());
        }

        var project = await _projectRepository.CreateProject(ct, currentUserId, args);
        if (args.Avatar is not null)
        {
            var avatarUrl = await _fileStorage.UploadFile(
                $"avatars/project/{project.Id}",
                args.Avatar.ContentType,
                args.Avatar.Stream);
            project = await _projectRepository.EditProjectAvatar(ct, project.Id, avatarUrl);
        }

        await _messageBus.Publish(project.ToCreatedEvent());
        return Result.Ok(project);
    }
}