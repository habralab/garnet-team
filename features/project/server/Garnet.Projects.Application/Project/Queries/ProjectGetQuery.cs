using FluentResults;
using Garnet.Projects.Application.Project.Errors;

namespace Garnet.Projects.Application.Project.Queries;

public class ProjectGetQuery
{
    private readonly IProjectRepository _projectRepository;

    public ProjectGetQuery(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<ProjectEntity>> Query(CancellationToken ct, string projectId)
    {
        var project = await _projectRepository.GetProject(ct, projectId);
        return project is null ? Result.Fail(new ProjectNotFoundError(projectId)) : Result.Ok(project);
    }
}