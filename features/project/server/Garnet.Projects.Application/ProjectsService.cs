using Garnet.Common.Application;

namespace Garnet.Projects.Application;

public class ProjectsService
{
    private readonly IProjectsRepository _repository;

    public ProjectsService(
        IProjectsRepository repository
    )
    {
        _repository = repository;
    }

    public async Task<Project> CreateProject(CancellationToken ct, ICurrentUserProvider currentUserProvider, string projectName, string? description)
    {
        return await _repository.CreateProject(ct, currentUserProvider.UserId, projectName, description);
    }
    
}