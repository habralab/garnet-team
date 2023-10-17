using FluentResults;
using Garnet.Projects.Application.Project.Args;

namespace Garnet.Projects.Application.Project.Queries;

public class ProjectsFilterQuery
{
    private readonly IProjectRepository _projectRepository;


    public ProjectsFilterQuery(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<ProjectEntity[]>> Query(CancellationToken ct, ProjectFilterArgs args)
    {
        return await _projectRepository.FilterProjects(ct, args);
    }
}