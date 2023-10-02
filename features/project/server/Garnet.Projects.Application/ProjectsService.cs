using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;

namespace Garnet.Projects.Application;

public class ProjectsService
{
    private readonly IProjectsRepository _repository;
    private readonly IMessageBus _messageBus;

    public ProjectsService(
        IProjectsRepository repository,
        IMessageBus messageBus
    )
    {
        _repository = repository;
        _messageBus = messageBus;
    }

    public async Task<Project> CreateProject(CancellationToken ct, ICurrentUserProvider currentUserProvider,
        string projectName, string? description)
    {
        return await _repository.CreateProject(ct, currentUserProvider.UserId, projectName, description);
    }

    public async Task<Project?> GetProject(CancellationToken ct, string projectId)
    {
        return await _repository.GetProject(ct, projectId);
    }
}