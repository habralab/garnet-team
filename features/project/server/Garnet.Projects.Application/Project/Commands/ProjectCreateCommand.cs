using FluentResults;
using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Args;
using Garnet.Projects.Application.Project.Errors;
using Garnet.Projects.Application.ProjectUser;

namespace Garnet.Projects.Application.Project.Commands;

public class ProjectCreateCommand
{
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectUserRepository _projectUserRepository;
    private readonly IMessageBus _messageBus;

    public ProjectCreateCommand(
        IProjectRepository projectRepository,
        IProjectUserRepository projectUserRepository,
        IMessageBus messageBus
    )
    {
        _projectRepository = projectRepository;
        _projectUserRepository = projectUserRepository;
        _messageBus = messageBus;
    }

    public async Task<Result<ProjectEntity>> Execute(CancellationToken ct, ProjectCreateArgs args)
    {
        var user = await _projectUserRepository.GetUser(ct, args.OwnerUserId);
        if (user is null)
        {
            return Result.Fail(new ProjectUserNotFoundError(args.OwnerUserId));
        }

        var project = await _projectRepository.CreateProject(ct, args);
        await _messageBus.Publish(project.ToCreatedEvent());
        return Result.Ok(project);
    }
}