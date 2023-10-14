using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Common.Application.S3;
using Garnet.Projects.Application.Project.Errors;

namespace Garnet.Projects.Application.Project.Commands;

public class ProjectUploadAvatarCommand
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IProjectRepository _projectRepository;
    private readonly IMessageBus _messageBus;
    private readonly IRemoteFileStorage _fileStorage;


    public ProjectUploadAvatarCommand(
        ICurrentUserProvider currentUserProvider,
        IProjectRepository projectRepository,
        IMessageBus messageBus, IRemoteFileStorage fileStorage)
    {
        _currentUserProvider = currentUserProvider;
        _projectRepository = projectRepository;
        _messageBus = messageBus;
        _fileStorage = fileStorage;
    }

    public async Task<Result<ProjectEntity>> Execute(
        CancellationToken ct,
        string projectId,
        string? contentType,
        Stream imageStream)
    {
        var project = await _projectRepository.GetProject(ct, projectId);

        if (project is null)
        {
            return Result.Fail(new ProjectNotFoundError(projectId));
        }

        if (project.OwnerUserId != _currentUserProvider.UserId)
        {
            return Result.Fail(new ProjectOnlyOwnerCanEditAvatarError());
        }

        var avatarUrl = await _fileStorage.UploadFile($"avatars/project/{project.Id}", contentType, imageStream);
        project = await _projectRepository.EditProjectAvatar(ct, project.Id, avatarUrl);

        await _messageBus.Publish(project.ToUpdatedEvent());
        return Result.Ok(project);
    }
}